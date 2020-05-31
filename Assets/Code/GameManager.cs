using System;
using Cinemachine;
using Code.Events.Core;
using Code.Interfaces;
using Code.Player.Human;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

namespace Code
{
    [Serializable]
    public struct TeamDefinition
    {
        public GameObject Team;
        public GameObject Champion;
        public PlayerInput Player;
    }

    public class GameManager : MonoBehaviour
    {
        public AtomicEvent gameOverEvent;
        private bool gameRunning = true;
        public AtomicEvent gameWonEvent;
        public GameObject playerContainer;
        private PlayerInputManager playerInputManager;
        public AtomicEvent playerWonEvent;

        public Timer resetTimer;
        public AtomicEvent roundStartedEvent;
        public Timer roundTimer;
        public TeamDefinition[] teamDefinitions;

        private void Awake()
        {
            playerInputManager = GetComponent<PlayerInputManager>();
            if (!Debug.isDebugBuild) Debug.unityLogger.logEnabled = false;
        }

        private void OnPlayerJoined(PlayerInput player)
        {
            var teamIndex = playerInputManager.playerCount - 1;
            var teamDefinition = teamDefinitions[teamIndex];
            teamDefinition.Player = player;

            teamDefinition.Player.name = $"Player {playerInputManager.playerCount.ToString()}";

            BindPlayerToChampion(player, teamDefinition);
            BindPlayerTimers(player, teamIndex);
            SetUpCamera(player);
            if (playerContainer != null) player.gameObject.transform.SetParent(playerContainer.transform);

            if (!roundTimer.IsRunning) NewRound();
        }

        private void SetUpCamera(PlayerInput player)
        {
            var mask = LayerMask.NameToLayer($"PlayerCam{playerInputManager.playerCount.ToString()}");
            var playerVirtualCameraCam = player.GetComponentInChildren<CinemachineVirtualCamera>();
            var playerCam = player.GetComponentInChildren<Camera>();

            playerVirtualCameraCam.gameObject.layer = mask;
            playerCam.gameObject.layer = mask;

            var otherPlayerMask = mask == 20 ? 21 : 20;
            playerCam.cullingMask = ~(1 << otherPlayerMask);
        }

        private void BindPlayerToChampion(PlayerInput player, TeamDefinition teamDefinition)
        {
            var targetTransform = teamDefinition.Champion.transform;
            var playerInputProxy = player.GetComponent<PlayerInputProxy>();
            var parentConstraint = player.GetComponent<ParentConstraint>();
            var constraintSource = new ConstraintSource {sourceTransform = targetTransform, weight = 1f};
            parentConstraint.AddSource(constraintSource);
            playerInputProxy.UpdateTeam(teamDefinition.Team);
        }

        private void BindPlayerTimers(PlayerInput player, int teamIndex)
        {
            var timers = player.GetComponentsInChildren<TextMeshProUGUI>();
            roundTimer.counterTextUIs[teamIndex] = timers[0];
            resetTimer.counterTextUIs[teamIndex] = timers[1];
        }

        public void ShowTitleScreen()
        {
            var humans = GameObject.FindGameObjectsWithTag("HumanPlayer");
            foreach (var human in humans) Destroy(human);
        }

        public void NewRound()
        {
            roundStartedEvent.Trigger();
            roundTimer.StartTimer();
            resetTimer.StopTimer();
        }

        public void RoundOver()
        {
            Debug.Log("Round Over");

            var playerWon = false;
            foreach (var team in teamDefinitions)
            {
                var progress = team.Team.GetComponent<IProgressInspector>();

                if (progress != null && !playerWon)
                {
                    Debug.Log($"{team.Team.name} Won => {progress.HasReachedTarget().ToString()}");
                    playerWon = progress.HasReachedTarget();
                }
            }

            var winEvent = (playerWon) ? playerWonEvent : gameWonEvent;
                winEvent.Trigger();

            GameOver();
        }

        private void GameOver()
        {
            roundTimer.StopTimer();
            resetTimer.StartTimer();
            gameOverEvent.Trigger();
        }
    }
}
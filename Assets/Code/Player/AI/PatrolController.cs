using System;
using Code.Movement;
using UnityEngine;
using CharacterController = Code.Movement.CharacterController;
using Random = UnityEngine.Random;

namespace Code.Player.AI
{
    public class PatrolController : MonoBehaviour
    {
        private enum AiAction
        {
            Jump,
            Attack,
            Alert
        }

        private enum AiMovement
        {
            Left,
            Right,
            Stop
        }

        private CharacterController characterController;
        private AiMovement currentMovement;
        private bool isStuckCheckRunning;
        private GroundChecker jumpCheck;
        private Direction lastDirection;
        private LeftRightController leftRightController;
        private bool patrolling;
        private GroundChecker turnChecker;
        private AiMovement PickMovement() => (AiMovement) Random.Range(0, 3);

        private void SetMovement(AiMovement movement)
        {
            switch (movement)
            {
                case AiMovement.Left:
                    characterController.Left();
                    break;
                case AiMovement.Right:
                    characterController.Right();
                    break;
                case AiMovement.Stop:
                    characterController.Right();
                    // characterController.Stop();
                    break;
            }
        }

        private void Start()
        {
            characterController = GetComponent<CharacterController>();

            var groundCheckers = GetComponentsInChildren<GroundChecker>();
            foreach (var checker in groundCheckers)
                if (checker.name.ToLower().Contains("sensor"))
                    turnChecker = checker;
                else
                    jumpCheck = checker;

            currentMovement = PickMovement();

            turnChecker.LeftSurface += () => { currentMovement = Turn(); };
        }

        private AiMovement Turn() => currentMovement == AiMovement.Right ? AiMovement.Left : AiMovement.Right;

        private void Update() => SetMovement(currentMovement);

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("PatrolTurnPoint")) currentMovement = Turn();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Level Tiles")) return;
            
            currentMovement = Turn();
        }

    }
}
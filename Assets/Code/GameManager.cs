using Code.Interfaces;
using Code.Player.Human;
using UnityEngine;

namespace Code
{
    public class GameManager : MonoBehaviour
    {

        public GameObject[] players;
        private bool gameRunning = true;
    
        void Start()
        {
            Debug.Log(players.Length);
        }

        void Update()
        {
        
        }

        public void TimerDone()
        {
            if (gameRunning)
            {
                gameRunning = false;
                Debug.Log("Game Won");
            }
            else
            {
                Debug.Log("Timer Finished but the game was won by player");
            }
        }
    
        public void LevelComplete()
        {
            if (!gameRunning)
            {
                Debug.Log("Timer Finished but the game was won by player");
            }
            else
            {
                Debug.Log("Game Won");
            
                var playerWon = false; 
                foreach (var player in players)
                {
                    var inputProxy = player.GetComponent<PlayerInputProxy>();
                
                    if (inputProxy.team)
                    {
                        var completor = inputProxy.team.GetComponent<ICompletor>();
                        Debug.Log($"{inputProxy.team.tag} Won => {(completor.TargetReached()).ToString()}");
                        playerWon = true;
                    }
                    
                    if (!playerWon) Debug.LogWarning("Nobody won");
                }
            }
            
        }
    }
}

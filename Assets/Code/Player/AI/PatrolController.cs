using Code.Movement;
using UnityEngine;
using UnityEngine.InputSystem;
using CharacterController = Code.Movement.CharacterController;

namespace Code.Player.AI
{
    public enum AiMovement
    {
        Left,
        Right,
        Stop
    }

    public enum AiAction
    {
        Jump,
        Attack,
        Alert
    }

//
// public class CardiganState
// {
// }
//
// public class CardiganPatrolState:CardiganState
// {
//     private Direction heading;
//     
//     public void OnEnter()
//     {
//         
//     }
//     
//     public void OnExit()
//     {
//         
//     }
//
// }
// public class CardiganAttackState:CardiganState
// {
//     private int heading;
//     public Descision Pressing { get; private set; }
//
//     private List<Descision> actions = new List<Descision>
//     {
//         Descision.Stop,
//         Descision.Attack
//     };
//
//     public void updatePressing()
//     {
//         
//     }
//     
//     public void OnExit()
//     {
//         
//     }
//
// }
//
// public class CardiganBrain
// {
//     
//     
//     
//
//  
//     public void Interaction(Collider2D other, LeftRightController leftRightController)
//     {
//         switch (other.tag)
//         {
//             case "Player":
//                 // if did see  Attack
//                 // else Alert then Attack
//                 break;
//
//             default: 
//                 // Turn
//                 break;
//         }
//     }
// }

    public class PatrolController : MonoBehaviour
    {
        private bool isStuckCheckRunning;
        private GroundChecker jumpCheck;
        private Direction lastDirection;
        private LeftRightController leftRightController;
        private CharacterController characterController;
        private bool patrolling;
        private GroundChecker turnChecker;
        private AiMovement currentMovement;
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

        private void Awake()
        {
          

        }

        private void Start()
        {
            characterController = GetComponent<CharacterController>();

            var groundCheckers = GetComponentsInChildren<GroundChecker>();
            foreach (var checker in groundCheckers)
            {
                if (checker.name.ToLower().Contains("sensor"))
                    turnChecker = checker;
                else
                    jumpCheck = checker;
            }

            currentMovement = PickMovement();

            turnChecker.LeftSurface += () => 
            {
                // Debug.Log("Must Turn");
                currentMovement =  Turn();
              //  Debug.Log("Should Turn");
            };
            
           
            // sideRightCheck.LeftSurface += () =>
            // {
            //     currentMovement = AiMovement.Left;
            //     characterController.Left();
            // };
        }

        private AiMovement Turn()
        {
            return currentMovement == AiMovement.Right ? AiMovement.Left: AiMovement.Right;
        }
        private void Update()
        {
            // transform.Translate(new Vector2(0.3f, 0));
          
           // if (!turnChecker.Check())
           // {
           //     Turn();
           // }
           // if (!jumpCheck.Check())
           // {
           //     characterController.Jump();
           //     Turn();
           // }
           // Debug.Log(((int) currentMovement).ToString());
           SetMovement(currentMovement);

           // if (patrolling) return;
            //
            // leftRightController.HeadLeft();
            // patrolling = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("PatrolTurnPoint"))
            {
                currentMovement = Turn();
            }
            if (other.CompareTag("PatrolTurnPoint"))
            {
                currentMovement = Turn();
            }


        }

        private void OnTriggerExit2D(Collider2D other)
        {
            // sideLeftCheck.LeftSurface 
            // Debug.Log("Exit");
            // characterController.Turn();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            // if (other.CompareTag("Player"))
            // {
            //     leftRightController.Stop();
            // }
            // var pos = transform.position;
            // var hit = Physics2D.Raycast(new Vector3(pos.x,pos.y-1f,pos.z), Vector2.left * leftRightController.direction.AsFloat());
            //
            // Debug.DrawRay(new Vector3(pos.x,pos.y-1.1f,pos.z), Vector2.left * leftRightController.direction.AsFloat(), Color.red);
            //
            // if (!isStuckCheckRunning && (object) hit.collider != null)
            // {
            //     StartCoroutine(WaitAndCheckIfStuck(other));
            // }
        }

        // private IEnumerator WaitAndCheckIfStuck(Collider2D other)
        // {
        //     isStuckCheckRunning = true;
        //     yield return new WaitForSeconds(0.25f);
        //
        //     if (lastDirection == Direction.Left)
        //     {
        //         lastDirection = Direction.Right;
        //         leftRightController.HeadRight();
        //     }
        //     else
        //     {
        //         lastDirection = Direction.Left;
        //         leftRightController.HeadLeft();
        //     }
        //
        //     yield return new WaitForSeconds(0.1f);
        //     isStuckCheckRunning = false;
        // }
        //
        // private void OnCollisionEnter2D(Collision2D other)
        // {
        //     if (other.collider.CompareTag("PatrolTurnPoint") || other.collider.CompareTag("Level Tiles"))
        //         leftRightController.Turn();
        // }
    }
}
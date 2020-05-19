using System.Collections;
using System.Collections.Generic;
using Code.Movement;
using UnityEngine;

public enum Descision {
    Left,
    Right,
    Stop,
    Jump,
    Attack
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
    private Direction lastDirection;
    private LeftRightController leftRightController;
    private bool patrolling;

    private void Awake()
    {
        leftRightController = GetComponent<LeftRightController>();
    }

    private void Start()
    {
    }
    
    private void Update()
    {
        if (patrolling) return;

        leftRightController.HeadLeft();
        patrolling = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PatrolTurnPoint") || other.CompareTag("Level Tiles")) leftRightController.Turn();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            leftRightController.Stop();
        }
        var pos = transform.position;
        var hit = Physics2D.Raycast(new Vector3(pos.x,pos.y-1f,pos.z), Vector2.left * leftRightController.direction.AsFloat());
        
        Debug.DrawRay(new Vector3(pos.x,pos.y-1.1f,pos.z), Vector2.left * leftRightController.direction.AsFloat(), Color.red);

        if (!isStuckCheckRunning && (object) hit.collider != null)
        {
            StartCoroutine(WaitAndCheckIfStuck(other));
        }
    }

    private IEnumerator WaitAndCheckIfStuck(Collider2D other)
    {
        isStuckCheckRunning = true;
        yield return new WaitForSeconds(0.25f);
       
        if (lastDirection == Direction.Left)
        {
            lastDirection = Direction.Right;
            leftRightController.HeadRight();
        }
        else
        {
            lastDirection = Direction.Left;
            leftRightController.HeadLeft();
        }
        
        yield return new WaitForSeconds(0.1f);
        isStuckCheckRunning = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("PatrolTurnPoint") || other.collider.CompareTag("Level Tiles"))
            leftRightController.Turn();
    }
}
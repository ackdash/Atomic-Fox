using Code.Interfaces;
using Code.Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.CharacterControl.Human
{
    public class PlayerInputProxy : MonoBehaviour
    {
        public GameObject team;
        private CharacterController characterMovementController;
       
        private void Start()
        {
            characterMovementController = team.GetComponentInChildren<CharacterController>();
        }

        public void OnAttack(InputValue btn) => characterMovementController.Attack(btn);

        public void OnJump() => characterMovementController.Jump();

        public void OnLeft(InputValue btn) => characterMovementController.Left(btn);

        public void OnRight(InputValue btn) => characterMovementController.Right(btn);

        public void OnStop(InputValue btn) => characterMovementController.Stop(btn);

        public void OnTurn() => characterMovementController.Turn();
    }
}
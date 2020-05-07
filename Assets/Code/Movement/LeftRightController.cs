using System;
using UnityEngine;

namespace Code.Movement
{
    public class LeftRightController : MonoBehaviour //, ILRMoveable
    {
        public int direction;

        [SerializeField] [InspectorName("Speed")]
        private float speed;

        private SpeedController speedController;

        private void Awake()
        {
            speedController = GetComponent<SpeedController>();
            if (speedController != null) speedController.SetDefaultSpeed(speed);
        }


        public void OnLeft()
        {
            direction = Direction.Left;
        }

        public void OnRight()
        {
            direction = Direction.Right;
        }

        public void OnTurn()
        {
            direction *= -1;
        }


        private void Update()
        {
            var h = direction * Time.deltaTime;
            var spd = speedController ? speedController.CurrentSpeed : speed;
            transform.Translate(Vector2.left * (h * spd), Space.World);
        }

        private struct Direction
        {
            public static int Left { get; } = -1;
            public static int Right { get; } = 1;
        }
    }
}
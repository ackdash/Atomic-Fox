using System;
using Code.Events.Core;
using Code.Interfaces.Game;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Mechanics
{


    public class LeftRightMover : MonoBehaviour//, ILRMoveable
    {
       
        public int direction;

        [SerializeField] [InspectorName("Speed")]
        private float speed;

        public struct Direction
        {
            public static int Left { get; } = -1;
            public static int Right { get; } = 1;
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
            transform.Translate(Vector2.left * (h * speed), Space.World);
        }
    }
}
using Code.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Movement
{
    public class LeftRightController : MonoBehaviour, ICurrentSpeed
    {
        private Animator animator;
        private int direction;
        private bool leftIsDown;
        private bool rightIsDown;

        [SerializeField] [InspectorName("Speed")]
        private float speed;

        private SpeedController speedController;

        public float CurrentSpeed => speedController.CurrentSpeed * direction;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            speedController = GetComponent<SpeedController>();
            if (speedController != null) speedController.SetDefaultSpeed(speed);
        }

        private void Update()
        {
            if (direction == Direction.None) return;

            var h = direction * Time.deltaTime;
            var spd = speedController ? speedController.CurrentSpeed : speed;

            transform.Translate(Vector2.left * (h * spd), Space.World);
        }

        public void OnAutoLeft(InputValue btn)
        {
            direction = Direction.Left;
        }

        public void OnAutoRight(InputValue btn)
        {
            direction = Direction.Right;
        }

        public void OnTurn()
        {
            direction *= -1;
        }

        public void OnLeft(InputValue btn)
        {
            leftIsDown = btn.isPressed;

            if (leftIsDown) HeadLeft();
            else if (rightIsDown) HeadRight();
            else if (!leftIsDown && !rightIsDown) Stop();
        }

        public void OnRight(InputValue btn)
        {
            rightIsDown = btn.isPressed;

            if (rightIsDown) HeadRight();
            else if (leftIsDown) HeadLeft();
            else if (!leftIsDown && !rightIsDown) Stop();
        }

        public void OnStop()
        {
            Stop();
        }

        private void HeadLeft()
        {
            var t = transform;
            var ls = t.localScale;
            direction = Direction.Left;
            t.localScale = new Vector3(-direction, ls.y, ls.z);
            animator.SetFloat("HorizontalSpeed", 1f);
        }

        private void HeadRight()
        {
            var t = transform;
            var ls = t.localScale;
            direction = Direction.Right;
            t.localScale = new Vector3(-direction, ls.y, ls.z);
            animator.SetFloat("HorizontalSpeed", 1f);
        }

        private void Stop()
        {
            animator.SetFloat("HorizontalSpeed", 0);
            direction = Direction.None;
        }

        private struct Direction
        {
            public static int Left { get; } = -1;
            public static int Right { get; } = 1;
            public static int None { get; } = 0;
        }
    }
}
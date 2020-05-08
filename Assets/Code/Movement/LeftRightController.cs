using Code.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Movement
{
    public class LeftRightController : MonoBehaviour, ICurrentSpeed
    {
        private Animator animator;
        private SpriteRenderer spriteRenderer;
        public int direction;

        private bool leftIsDown;
        private bool rightIsDown;

        [SerializeField] [InspectorName("Speed")]
        private float speed;

        private SpeedController speedController;

        public float CurrentSpeed => speedController.CurrentSpeed * direction;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            speedController = GetComponent<SpeedController>();
            if (speedController != null) speedController.SetDefaultSpeed(speed);
        }

        public void OnLeft(InputValue btn)
        {
            leftIsDown = btn.isPressed;

            if (leftIsDown)
            {
                var ls = transform.localScale;
                direction = Direction.Left;
                transform.localScale = new Vector3(-direction,ls.y, ls.z);
                animator.SetFloat("HorizontalSpeed", 1f);
                
            }
            else if (!rightIsDown)
            {
                OnStop();
            }
        }

        public void OnRight(InputValue btn)
        {
            rightIsDown = btn.isPressed;

            if (rightIsDown)
            {
                var ls = transform.localScale;
                direction = Direction.Right;
                transform.localScale = new Vector3(-direction ,ls.y, ls.z);
               
                animator.SetFloat("HorizontalSpeed", 1f);
            }
            else if (!leftIsDown)
            {
                OnStop();
            }
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

        public void OnStop()
        {
            animator.SetFloat("HorizontalSpeed", 0);
            direction = Direction.None;
        }

        private void Update()
        {
            if (direction == Direction.None) return;

            var h = direction * Time.deltaTime;
            var spd = speedController ? speedController.CurrentSpeed : speed;

            transform.Translate(Vector2.left * (h * spd), Space.World);
        }

        private struct Direction
        {
            public static int Left { get; } = -1;
            public static int Right { get; } = 1;
            public static int None { get; } = 0;
        }
    }
}
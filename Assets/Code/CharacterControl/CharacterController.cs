using System.Linq;
using Code.Interfaces;
using Code.Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.CharacterControl
{
    public class CharacterController : MonoBehaviour
    {
        private static readonly int AnimatorHorizontalSpeed = Animator.StringToHash("HorizontalSpeed");
        private static readonly int AnimatorFallSpeed = Animator.StringToHash("FallSpeed");
        private static readonly int AnimatorIsJumping = Animator.StringToHash("IsJumping");

        private Animator animator;
        private IAttacker attackController;
        private CharacterGravityController characterGravityController;
        [SerializeField] private bool enableSpeedController = true;
        private FallChecker fallChecker;
        private GroundChecker groundChecker;
        private JumpController jumpController;
        private bool leftIsDown;
        private LeftRightController leftRightController;
        private Rigidbody2D rb;
        private bool rightIsDown;
        private SpeedController speedController;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            jumpController = GetComponent<JumpController>();
            leftRightController = GetComponent<LeftRightController>();
            speedController = GetComponent<SpeedController>();
            attackController = GetComponent<IAttacker>();
            groundChecker = GetComponentInChildren<GroundChecker>();
            fallChecker = GetComponent<FallChecker>();
            characterGravityController = GetComponent<CharacterGravityController>();
            speedController.enabled = enableSpeedController;

            animator = GetComponentsInChildren<Animator>()
                .First(r => r.CompareTag("CharacterGFX"));

            jumpController.JumpEnded += () => OnJumpEnded();
        }

        private void Update()
        {
            var tp = transform.position;

            var horizontalTranslation = leftRightController.Calculate();
            var verticalTranslation = jumpController.Calculate();
            var fallSpeed = fallChecker.Calculate();

            if (fallChecker.IsFalling || !groundChecker.IsOnGround) characterGravityController.ApplyGravity();

            transform.Translate(new Vector2(horizontalTranslation, verticalTranslation));

            animator.speed = enableSpeedController ? speedController.CurrentSpeedNormalised : animator.speed;
            animator.SetFloat(AnimatorFallSpeed, fallSpeed);
        }

        public void Attack(InputValue btn) => attackController.Attack(btn);

        public void Jump()
        {
            if (jumpController.IsJumping || fallChecker.IsFalling || !groundChecker.Check()) return;
            characterGravityController.DisableGravity();
            groundChecker.Reset();
            animator.SetBool(AnimatorIsJumping, true);
            jumpController.Jump();
        }

        private void OnJumpEnded()
        {
            animator.SetBool(AnimatorIsJumping, false);
            characterGravityController.ApplyGravity();
        }

        public void Left(InputValue btn)
        {
            leftIsDown = btn.isPressed;

            if (leftIsDown) leftRightController.HeadLeft();
            else if (rightIsDown) leftRightController.HeadRight();
            else if (!leftIsDown && !rightIsDown) leftRightController.Stop();

            animator.SetFloat(AnimatorHorizontalSpeed, leftRightController.HorizontalSpeed);
        }

        public void Right(InputValue btn)
        {
            rightIsDown = btn.isPressed;

            if (rightIsDown) leftRightController.HeadRight();
            else if (leftIsDown) leftRightController.HeadLeft();
            else if (!leftIsDown && !rightIsDown) leftRightController.Stop();

            animator.SetFloat(AnimatorHorizontalSpeed, leftRightController.HorizontalSpeed);
        }

        public void Stop(InputValue btn)
        {
            leftRightController.Stop();
            animator.SetFloat(AnimatorHorizontalSpeed, leftRightController.HorizontalSpeed);
        }

        public void Turn()
        {
            leftRightController.Turn();
            animator.SetFloat(AnimatorHorizontalSpeed, leftRightController.HorizontalSpeed);
        }
    }
}
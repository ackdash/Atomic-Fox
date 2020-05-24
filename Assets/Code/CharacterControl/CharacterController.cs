using System.Linq;
using Code.Interfaces;
using Code.ItemCollection;
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
        private static readonly int AnimatorIsCarrying = Animator.StringToHash("IsCarrying");
        private static readonly int IsAttacked = Animator.StringToHash("IsAttacked");

        private Animator animator;
        private IAttacker attackController;
        private IAttacked attackedController;
        private CharacterGravityController characterGravityController;
        [SerializeField] private bool speedControllerEnabled = true;
        private FallChecker fallChecker;
        private GroundChecker groundChecker;
        private ItemCollector itemCollector;
        private JumpController jumpController;
        private bool leftIsDown;
        private LeftRightController leftRightController;
        private Rigidbody2D rb;
        private bool rightIsDown;
        private SpeedController speedController;

        private bool hasItemCollector;
        private bool hasAttackController;
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            jumpController = GetComponent<JumpController>();
            leftRightController = GetComponent<LeftRightController>();
            speedController = GetComponent<SpeedController>();
            attackController = GetComponent<IAttacker>();
            attackedController = GetComponent<IAttacked>();
            groundChecker = GetComponentInChildren<GroundChecker>();
            fallChecker = GetComponent<FallChecker>();
            characterGravityController = GetComponent<CharacterGravityController>();
            speedController.enabled = speedControllerEnabled;

            itemCollector = GetComponentInChildren<ItemCollector>();
            animator = GetComponentsInChildren<Animator>()
                .First(r => r.CompareTag("CharacterGFX"));

            if (itemCollector != null)
            {
                hasItemCollector = true;
                itemCollector.ItemCollected += () => OnCollectedItem();
                itemCollector.ItemDropped += () => OnDroppedItem();
                
            }

            if (attackedController != null)
            {
                hasAttackController = true;
                attackedController.UnderAttack += direction => OnAttacked(direction);
                attackedController.AttackFinished += () => OnAttackFinished();
            }
            
            jumpController.JumpEnded += () => OnJumpEnded();
        }

        private void OnAttacked(Direction direction)
        {
            itemCollector.CanCollect = false;
            animator.SetBool(IsAttacked, true);
            jumpController.CancelJump();
            if (hasItemCollector && itemCollector.HasItems)
            {
                itemCollector.DropItems();
            }
        }

        private void OnAttackFinished()
        {
            itemCollector.CanCollect = true;
            animator.SetBool(IsAttacked, false);
        }

        private void OnCollectedItem()
        {
            animator.SetBool(AnimatorIsCarrying, true);
        }

        private void OnDroppedItem()
        {
            animator.SetBool(AnimatorIsCarrying, false);
        }

        private void Update()
        {
            var tp = transform.position;
          
            var horizontalTranslation = leftRightController.Calculate();
            var verticalTranslation = jumpController.Calculate();
            var fallSpeed = fallChecker.Calculate();

            if (fallChecker.IsFalling || !groundChecker.IsOnGround) characterGravityController.ApplyGravity();

            if (fallChecker.IsFallingTooFast && hasItemCollector && itemCollector.HasItems) itemCollector.DropItems();
            
            if (hasAttackController && !attackedController.IsUnderAttack)
                transform.Translate(new Vector2(horizontalTranslation, verticalTranslation));

            animator.speed = speedControllerEnabled ? speedController.CurrentSpeedNormalised : animator.speed;
            animator.SetFloat(AnimatorFallSpeed, fallSpeed);
        }

        public void Attack(InputValue btn) => attackController?.Attack(btn);

        public void Jump()
        {
            if (hasAttackController && attackedController.IsUnderAttack
                || jumpController.IsJumping
                || fallChecker.IsFalling
                || !groundChecker.Check()) return;

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
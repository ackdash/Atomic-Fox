using System.Linq;
using Code.Interfaces;
using Code.Interfaces.Game;
using Code.ItemCollection;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Movement
{
    public class CharacterController : MonoBehaviour, IResetable
    {
        private static readonly int AnimatorHorizontalSpeed = Animator.StringToHash("HorizontalSpeed");
        private static readonly int AnimatorFallSpeed = Animator.StringToHash("FallSpeed");
        private static readonly int AnimatorIsJumping = Animator.StringToHash("IsJumping");
        private static readonly int AnimatorIsCarrying = Animator.StringToHash("IsCarrying");
        private static readonly int IsAttacked = Animator.StringToHash("IsAttacked");
        private static readonly int IsAlert = Animator.StringToHash("IsAlert");

        private Animator animator;
        private IAttacker attackController;
        private IAttacked attackedController;
        private float bufferedJump = -10f;
        private bool canGhostJump = true;
        private CharacterGravityController characterGravityController;
        private FallChecker fallChecker;
        private float ghostJump;
        [SerializeField] private float ghostJumpBuffer;
        private GroundChecker groundChecker;
        private bool hasAttackController;
        private bool hasItemCollector;
        private ItemCollector itemCollector;
        [SerializeField] private float jumpBuffer;
        private JumpController jumpController;
        private bool leftIsDown;
        private LeftRightController leftRightController;
        private Rigidbody2D rb;
        private bool rightIsDown;
        private SpeedController speedController;
        [SerializeField] private bool speedControllerEnabled = true;
        private Vector2 spawnPoint;
        private float spawnPointYOffset;

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
                attackedController.AttackFinished += () => OnNoLongerAttack();
            }

            jumpController.JumpEnded += () => OnJumpEnded();

            fallChecker.StartedFalling += () => OnStartedFalling();

            spawnPoint = new Vector2(transform.position.x, transform.position.y + spawnPointYOffset);
        }

        public void Attack(InputValue btn) => attackController?.Attack(btn);

        private void OnAttacked(Direction direction)
        {
            itemCollector.CanCollect = false;
            characterGravityController.ApplyGravity();
            jumpController.CancelJump();
            jumpController.SetNotJumpmping();
            bufferedJump = -10f;
            animator.SetBool(IsAttacked, true);            
            animator.SetBool(AnimatorIsJumping, false);
            animator.SetBool(AnimatorIsCarrying, false);
            if (hasItemCollector && itemCollector.HasItems) itemCollector.DropItems();
        }

        private void OnNoLongerAttack()
        {
            itemCollector.CanCollect = true;
            animator.SetBool(IsAttacked, false);
        }

        private void OnCollectedItem() => animator.SetBool(AnimatorIsCarrying, true);

        private void OnDroppedItem() => animator.SetBool(AnimatorIsCarrying, false);

        private void OnStartedFalling()
        {
            animator.SetBool(AnimatorIsJumping, false);
            jumpController.SetNotJumpmping();
            characterGravityController.ApplyGravity();
            ghostJump = Time.frameCount;
        }

        private void Update()
        {
            var horizontalTranslation = leftRightController.Calculate();
            var verticalTranslation = jumpController.Calculate();
            var fallSpeed = fallChecker.Calculate();
           
            if (
                !(hasAttackController && attackedController.IsUnderAttack
                    ) && !jumpController.IsJumping && !fallChecker.IsFalling && groundChecker.Check() &&
                Time.frameCount - bufferedJump < jumpBuffer)
            {
                Jump(false);
                bufferedJump = -10f;
            }

            if (!jumpController.IsJumping && groundChecker.Check()) canGhostJump = true;

            if (fallChecker.IsFallingTooFast && hasItemCollector && itemCollector.HasItems)
            {
                animator.SetBool(AnimatorIsCarrying, false);
                itemCollector.DropItems();
            }

            if (hasAttackController && !attackedController.IsUnderAttack || !hasAttackController)
                transform.Translate(new Vector2(horizontalTranslation, verticalTranslation));

            animator.speed = speedControllerEnabled ? speedController.CurrentSpeedNormalised : animator.speed;
            animator.SetFloat(AnimatorFallSpeed, fallSpeed);
        }


        public void Jump(bool canBuffer = true)
        {
            if (canGhostJump && Time.frameCount < ghostJump + ghostJumpBuffer)
            {
                fallChecker.DisableFalling();
            }
            else if (
                canBuffer && (hasAttackController && attackedController.IsUnderAttack
                              || fallChecker.IsFalling
                              || !groundChecker.Check()))
            {
                bufferedJump = Time.frameCount;
                return;
            }

            canGhostJump = false;
            ghostJump = 0f;
            characterGravityController.DisableGravity();
            groundChecker.Reset();
            animator.SetBool(AnimatorIsJumping, true);
            jumpController.Jump();
        }

        private void OnJumpEnded()
        {
            fallChecker.EnableFalling();
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            animator.SetBool(AnimatorIsJumping, false);
            characterGravityController.ApplyGravity();
        }

        public void Left()
        {
            rightIsDown = false;
            leftIsDown = true;
            leftRightController.HeadLeft();
            animator.SetFloat(AnimatorHorizontalSpeed, leftRightController.HorizontalSpeed);
        }

        public void Left(InputValue btn)
        {
            leftIsDown = btn.isPressed;

            if (leftIsDown) leftRightController.HeadLeft();
            else if (rightIsDown) leftRightController.HeadRight();
            else if (!leftIsDown && !rightIsDown) leftRightController.Stop();

            animator.SetFloat(AnimatorHorizontalSpeed, leftRightController.HorizontalSpeed);
        }

        public void Right()
        {
            rightIsDown = true;
            leftIsDown = false;
            leftRightController.HeadRight();
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

        public void Stop()
        {
            rightIsDown = false;
            leftIsDown = false;
            leftRightController.Stop();
            animator.SetFloat(AnimatorHorizontalSpeed, leftRightController.HorizontalSpeed);
        }

        public void Stop(InputValue btn) => Stop();

        public void Turn()
        {
            leftRightController.Turn();
            animator.SetFloat(AnimatorHorizontalSpeed, leftRightController.HorizontalSpeed);
        }

        public void Reset()
        {
            characterGravityController.ApplyGravity();
            jumpController.CancelJump();
            jumpController.SetNotJumpmping();
            bufferedJump = -10f;
            animator.SetBool(IsAttacked, false);            
            animator.SetBool(AnimatorIsJumping, false);
            animator.SetBool(AnimatorIsCarrying, false);
            if (hasItemCollector && itemCollector.HasItems)
            {
                itemCollector.CanCollect = true;
                itemCollector.DropItems();
            }
            transform.position = spawnPoint;
        }
    }
}
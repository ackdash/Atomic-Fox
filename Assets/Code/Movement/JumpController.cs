using UnityEngine;

namespace Code.Movement
{
    public class JumpController : MonoBehaviour
    {
        private Animator animator;
        private FallChecker fallChecker;
        private float gravityScaleCached;
        private GroundChecker groundChecker;

        [SerializeField] [InspectorName("Speed")]
        private bool isJumping;

        private float jumpCachedPosY;
        public float JumpDeltaProp;
        private Rigidbody2D rb;
        private SpeedController speedController;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            fallChecker = GetComponent<FallChecker>();
            speedController = GetComponent<SpeedController>();
            groundChecker = GetComponentInChildren<GroundChecker>();

            jumpCachedPosY = transform.position.y;
            gravityScaleCached = rb.gravityScale;
        }

        private void Update()
        {
            var t = transform;
            var p = t.position;

            if (fallChecker.IsFalling)
            {
                animator.SetFloat("FallSpeed", fallChecker.FallSpeed);
                rb.gravityScale = gravityScaleCached * speedController.CurrentSpeedNormalised;
            }
            else
            {
                animator.SetFloat("FallSpeed", fallChecker.FallSpeed);
            }

            if (!isJumping) return;

            animator.speed = speedController.CurrentSpeedNormalised;
            t.position = new Vector2(p.x, jumpCachedPosY + JumpDeltaProp);
        }

        private void PrepForJump()
        {
            jumpCachedPosY = transform.position.y;
            rb.gravityScale = 0f;
            groundChecker.Reset();
        }

        private void Jump()
        {
            PrepForJump();
            Debug.Log("Jump");
            isJumping = true;
            animator.SetBool("IsJumping", isJumping);
        }

        private void EndJump()
        {
            rb.gravityScale = gravityScaleCached;
            isJumping = false;
            animator.SetBool("IsJumping", isJumping);
        }

        public void OnJump()
        {
            if (!isJumping && !fallChecker.IsFalling && groundChecker.IsOnGround) Jump();
        }

        public void OnJumpFinished()
        {
            EndJump();
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (isJumping) EndJump();
        }
    }
}
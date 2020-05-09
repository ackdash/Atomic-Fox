using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Movement
{
    public class JumpsController : MonoBehaviour
    {
        private Animator animator;
        public LayerMask Ground;

        private Transform groundChecker;

        public float GroundDistance = 0.2f;
        private bool isGrounded;

        [SerializeField] [InspectorName("Speed")]
        private bool isJumping;

        public float JumpHeight = 2f;

        public float JumpSpeed;
        private Rigidbody2D rb;
        // private SpeedController speedController;
        public float yPosAtJump;
        public float yPosTargetHeightJump;
        // public float CurrentSpeed => speedController.CurrentSpeed;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            // speedController = GetComponent<SpeedController>();
            // if (speedController != null) speedController.SetDefaultSpeed(JumpSpeed);

            groundChecker = transform.GetChild(0);
        }

        private void Update()
        {
            // Debug.Log((Vector2.up * Mathf.Sqrt(JumpHeight * -2f * Physics2D.gravity.y)).ToString());
            isGrounded = Physics.CheckSphere(groundChecker.position, GroundDistance, Ground,
                QueryTriggerInteraction.Ignore);
            // if (!isGrounded) isJumping = false;
            // && isGrounded , ForceMode2D.Force  && isGrounded
            // Debug.Log($"{isJumping.ToString()} {isGrounded.ToString()}");  && rb.velocity.y > 0f
            // Debug.Log(rb.velocity.y.ToString());
            if (isJumping && rb.velocity.y < 10f && transform.position.y < yPosTargetHeightJump)
                rb.AddForce(Vector2.up * JumpSpeed);
            else isJumping = false;
            if (transform.position.y > yPosTargetHeightJump)
            {
                rb.AddForce(Vector2.down * JumpSpeed * 2);
                rb.velocity = Vector2.zero;
            }
           
        }

        public void OnJump(InputValue btn)
        {
            yPosAtJump = transform.position.y;
            yPosTargetHeightJump = yPosAtJump + JumpHeight;
            Debug.Log($"Jump! {yPosAtJump} {yPosTargetHeightJump}");
            isJumping = true;// btn.isPressed;
        }
    }
}
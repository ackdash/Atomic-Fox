using UnityEngine;

namespace Code.Movement
{
    public class JumpController : MonoBehaviour
    {
        private Animator animator;


        [SerializeField] [InspectorName("Speed")]
        private bool isJumping;

        private float jumpCachedPosY;

        public float JumpDeltaProp;


        private void Awake()
        {
            jumpCachedPosY = transform.position.y;
            animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            var t = transform;
            var p = t.position;

            // if (isJumping)
                t.position = new Vector2(p.x, jumpCachedPosY + JumpDeltaProp);

                if (t.position.y < jumpCachedPosY)
                {
                    t.position = new Vector2(p.x, jumpCachedPosY);
                }
        }

        public void OnJump()
        {
            Debug.Log("Hit it");
            animator.SetBool("IsJumping", true);

            jumpCachedPosY = transform.position.y;
            isJumping = true; // btn.isPressed;
        }

        public void OnJumpFinished()
        {
            animator.SetBool("IsJumping", false);
            isJumping = false;
        }
    }
}
using System;
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

            if (!isJumping) return;
            
            t.position = new Vector2(p.x, jumpCachedPosY + JumpDeltaProp);

            if (t.position.y < jumpCachedPosY)
            {
                t.position = new Vector2(p.x, jumpCachedPosY);
            }
        }

        public void OnJump()
        {
            if (isJumping) return;
            
            animator.SetInteger("IsJumping", 1);

            jumpCachedPosY = transform.position.y;
            isJumping = true;
        }

        private void OnJumpFinished()
        {
            animator.SetInteger("IsJumping", 0);
            isJumping = false;
        }
        
        // TODO: OnGroundChecks
        private void OnCollisionEnter2D(Collision2D other)
        {
            OnJumpFinished();
        }
    }
}
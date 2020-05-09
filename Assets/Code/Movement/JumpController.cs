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
        private bool jumpPressed;

        private void Awake()
        {
            jumpCachedPosY = transform.position.y;
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            var t = transform;
            var p = t.position;

            if (isJumping) t.position = new Vector2(p.x, jumpCachedPosY + JumpDeltaProp);
            
        }

        public void OnJump()
        {
            print("s");
            animator.SetBool("IsJumping", true);
            
            jumpCachedPosY = transform.position.y;
            jumpPressed = true;
            isJumping = true; // btn.isPressed;
        }

        public void OnJumpFinished()
        {
            animator.SetBool("IsJumping", false);
            isJumping = false;
        }
    }
}
using System;
using UnityEngine;

namespace Code.Movement
{
    public class JumpController : MonoBehaviour
    {
        [SerializeField] private float jumpCancelThreshold = 1.25f;

        public float JumpDeltaProp;
        public float lastJumpDeltaProp;
        public bool IsJumping { get; private set; }

        public event Action JumpEnded;

        public float Calculate()
        {
            if (!IsJumping) return 0;

            var result = JumpDeltaProp - lastJumpDeltaProp;
            lastJumpDeltaProp = JumpDeltaProp;

            return result;
        }

        public void Jump() => IsJumping = true;

        public void OnJumpFinished() => EndJump();
        
        private void EndJump()
        {
            lastJumpDeltaProp = 0f;
            IsJumping = false;
            JumpEnded?.Invoke();
        }

        public void CancelJump() => EndJump();
      
        private void OnCollisionEnter2D()
        {
            if (IsJumping && JumpDeltaProp > jumpCancelThreshold) EndJump();
        }
    }
}
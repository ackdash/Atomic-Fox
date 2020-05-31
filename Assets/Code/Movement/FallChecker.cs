using System;
using UnityEngine;

namespace Code.Movement
{
    public class FallChecker : MonoBehaviour
    {
        private Rigidbody2D rb;
        public float fastFallSpeedThreshold;
        private bool canFall = true;
        public bool IsFalling { get; private set; }
        public bool IsFallingTooFast { get; private set; }

        private bool hasInvokedFallEvent;
        public event Action StartedFalling;
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public float Calculate()
        {
            if (!canFall)
            {
                IsFalling = false;
                IsFallingTooFast = false;
                return 0f;
            }
            
            var ySpeed = rb.velocity.y;
            IsFalling = ySpeed < 0f;
            IsFallingTooFast = IsFalling && -ySpeed > fastFallSpeedThreshold;

            if (!IsFalling)
            {
                hasInvokedFallEvent = false;
                return 0f;
            }

            if (!hasInvokedFallEvent)
            {
                StartedFalling?.Invoke();
                hasInvokedFallEvent = true;
            }

            return ySpeed * -1f; 
        }

        public void DisableFalling()
        {
            canFall = false;
        }
        public void EnableFalling()
        {
            canFall = true;
        }
    }
}
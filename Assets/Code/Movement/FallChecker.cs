using UnityEngine;

namespace Code.Movement
{
    public class FallChecker : MonoBehaviour
    {
        private Rigidbody2D rb;
        public float fastFallSpeedThreshold;
        
        public bool IsFalling { get; private set; }
        public bool IsFallingTooFast { get; private set; }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public float Calculate()
        {
            var ySpeed = rb.velocity.y;
            IsFalling = ySpeed < 0f;
            IsFallingTooFast = IsFalling && -ySpeed > fastFallSpeedThreshold;
            return IsFalling ? ySpeed * -1 : 0;
        }
    }
}
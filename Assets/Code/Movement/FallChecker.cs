using UnityEngine;

namespace Code.Movement
{
    public class FallChecker : MonoBehaviour
    {
        private Rigidbody2D rb;
        public bool IsFalling { get; private set; }
        public float FallSpeed { get; private set; }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            var ySpeed =rb.velocity.y;
            IsFalling = ySpeed < 0f;
            FallSpeed = IsFalling ? ySpeed * -1 : 0;
        }
    }
}
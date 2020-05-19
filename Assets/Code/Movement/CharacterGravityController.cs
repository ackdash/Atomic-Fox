using UnityEngine;

namespace Code.Movement
{
    public class CharacterGravityController : MonoBehaviour
    {
        private float gravityScaleCached;

        private Rigidbody2D rb;

        private SpeedController speedController;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            speedController = GetComponent<SpeedController>();
            gravityScaleCached = rb.gravityScale;
        }

        public float Calculate() => rb.velocity.y < 0 ? ScaleGravityInRelationToSpeed() : gravityScaleCached;

        private float ScaleGravityInRelationToSpeed() => gravityScaleCached * speedController.CurrentSpeedNormalised;

        public void DisableGravity() => rb.gravityScale = 0f;

        public void ApplyGravity() => rb.gravityScale = Calculate();
    }
}
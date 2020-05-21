using UnityEngine;

namespace Code.Movement
{
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] [InspectorName("Ground Tag")]
        private string groundTag;

        private bool isOnGround;

        [SerializeField] [InspectorName("Layer Mask")]
        private LayerMask layerMask;

        [SerializeField] [InspectorName("Distance to ground")]
        private float radius;

        public bool IsOnGround => isOnGround ? isOnGround : Check();

        public bool Check()
        {
            var check = Physics2D.OverlapCircle(transform.position, radius, layerMask);
            isOnGround = (object) check != null;
            return isOnGround;
        }

        public void Reset()
        {
            isOnGround = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!isOnGround && other.CompareTag(groundTag)) isOnGround = true;
        }
    }
}
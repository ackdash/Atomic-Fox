using System;
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

        [SerializeField] [InspectorName("Distance to ground")]
        private float yOffset = 0.25f;
        public event Action LeftSurface;

        public bool IsOnGround => isOnGround ? isOnGround : Check();

        public bool Check()
        {
            var checkPos = new Vector2(transform.position.x, transform.position.y + yOffset);
            // Debug.DrawLine();
            var check = Physics2D.OverlapCircle(checkPos, radius, layerMask);
            isOnGround = (object) check != null;
            return isOnGround;
        }

        private void Update()
        {
            var checkPos = new Vector2(transform.position.x, transform.position.y + yOffset);
            Debug.DrawLine(new Vector2(checkPos.x - radius / 2f, checkPos.y),
                new Vector2(checkPos.x + radius / 2f, checkPos.y), Color.magenta);
        }

        public void Reset()
        {
            isOnGround = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!isOnGround && other.CompareTag(groundTag)) isOnGround = true;
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            // Debug.Log(other.tag);
            if (other.CompareTag(groundTag))  LeftSurface?.Invoke();
           
        }
    }
}
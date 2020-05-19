using System.Linq;
using UnityEngine;

namespace Code.Movement
{
    public class FallChecker : MonoBehaviour
    {
        private Rigidbody2D rb;
        
        public bool IsFalling { get; private set; }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public float Calculate()
        {
            var ySpeed = rb.velocity.y;
            IsFalling = ySpeed < 0f;
            return IsFalling ? ySpeed * -1 : 0;
        }
    }
}
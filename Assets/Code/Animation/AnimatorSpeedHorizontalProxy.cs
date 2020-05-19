using UnityEngine;

namespace Code.Animation
{
    public class AnimatorSpeedHorizontalProxy : MonoBehaviour
    {
        private Animator animator;
        private Rigidbody2D rb;

        private float speedCache;
        private static readonly int HorizontalSpeed = Animator.StringToHash("HorizontalSpeed");

        private void Start()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            
            var speed = Mathf.Abs(rb.velocity.x) * 100000000f;
            animator.SetFloat(HorizontalSpeed, speed);
            
            if (!Mathf.Approximately(speed, speedCache))
            {
               
                speedCache = speed;
                
            }
        }
    }
}
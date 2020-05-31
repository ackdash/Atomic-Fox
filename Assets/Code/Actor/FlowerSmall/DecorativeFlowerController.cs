using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Code.Actor.FlowerSmall
{
    public class DecorativeFlowerController : MonoBehaviour
    {
        private static readonly int ShakeRight = Animator.StringToHash("ShakeRight");
        private static readonly int ShakeLeft = Animator.StringToHash("ShakeLeft");
        private Animator animator;
       
        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var impact = other.transform.position - transform.position;
            animator.SetBool(impact.x < 0f ? ShakeRight : ShakeLeft, true);
        }

        private void StopRightShake()
        {
            animator.SetBool(ShakeRight, false);
        }

        private void StopLeftShake()
        {
            animator.SetBool(ShakeLeft, false);
        }
    }
}
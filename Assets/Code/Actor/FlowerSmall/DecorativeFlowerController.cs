using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Code.Actor.FlowerSmall
{
    public class DecorativeFlowerController : MonoBehaviour
    {
        private static readonly int ShakeRight = Animator.StringToHash("ShakeRight");
        private static readonly int ShakeLeft = Animator.StringToHash("ShakeLeft");
        private Animator animator;
        public Light2D light;
        private Vector2 lightPos;

        private void Start()
        {
            animator = GetComponent<Animator>();
            lightPos = light.transform.position;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var impact = other.transform.position - transform.position;

            if (impact.x < 0f)
            {
                animator.SetBool(ShakeRight, true);
                light.transform.position = new Vector2(lightPos.x + 0.03f, lightPos.y);
            }
            else
            {
                animator.SetBool(ShakeLeft, true);
                light.transform.position = new Vector2(lightPos.x - 0.03f, lightPos.y);
            }
        }

        private void StopRightShake()
        {
            animator.SetBool(ShakeRight, false);
        }

        private void StopRightLightMove()
        {
            light.transform.position = new Vector2(lightPos.x - 0.03f, lightPos.y);
        }

        private void StopLeftShake()
        {
            animator.SetBool(ShakeRight, false);
            light.transform.position = new Vector2(lightPos.x + 0.03f, lightPos.y);

            animator.SetBool(ShakeLeft, false);
        }
    }
}
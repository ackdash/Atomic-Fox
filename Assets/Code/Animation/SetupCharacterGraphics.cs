using System.Linq;
using UnityEngine;

namespace Code.Animation
{
    public class SetupCharacterGraphics : MonoBehaviour
    {
        public Sprite defaultSprite;
        public RuntimeAnimatorController runtimeAnimationController;
 
        private void Start()
        {
            var animator = GetComponentsInChildren<Animator>()
                .First(r => r.CompareTag("CharacterGFX"));
            animator.runtimeAnimatorController = runtimeAnimationController;
            
            Debug.Log(animator.runtimeAnimatorController);
            var spriteRenderer = GetComponentsInChildren<SpriteRenderer>()
                .First(r => r.CompareTag("CharacterGFX"));

            if (defaultSprite) spriteRenderer.sprite = defaultSprite;
        }
    }
}
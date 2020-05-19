using System.Linq;
using Code.Interfaces;
using JetBrains.Annotations;
// using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.UI;

namespace Code.Movement
{
    public class CardiganAttackController : MonoBehaviour, IAttacker
    {
        private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
        private Animator animator;
        private LeftRightController leftRightController;
        public GameObject AttackProjectilePrefab;
        private GameObject attackProjectile;
        private bool isAttacking;
        private bool isProjectileActive; 
        private AnimationClip attackClip;
        // Start is called before the first frame update
        private void Awake()
        { 
            leftRightController = GetComponent<LeftRightController>();
            animator = GetComponentsInChildren<Animator>()
                .First(r => r.CompareTag("CharacterGFX"));
        }

        void Start()
        {
            attackProjectile = Instantiate(AttackProjectilePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            attackProjectile.SetActive(false);
        }
        
        void Update()
        {
            if (isAttacking && !attackProjectile.activeSelf)
            {
                FireProjectile();
            }
        }

        private void FireProjectile()
        {
            attackProjectile.transform.position = transform.position;
            attackProjectile.SetActive(true);
        }

        public void Attack(InputValue btn)
        {
            
            if (btn.isPressed)
            {
                Debug.Log("Bingo Button");
                attackProjectile.SetActive(false);
                leftRightController.Stop();
                isAttacking = true;
            }
            else
            {
                Debug.Log("No Bingo Button");
                isAttacking = false;
               
            }
            animator.SetBool(IsAttacking, isAttacking);
        }
    }
}

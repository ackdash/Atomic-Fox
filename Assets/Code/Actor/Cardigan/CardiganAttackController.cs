using System.Linq;
using Code.Interfaces;
using Code.Movement;
using UnityEngine;
using UnityEngine.InputSystem;
// using UnityEditor.Animations;

namespace Code.Actor.Cardigan
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

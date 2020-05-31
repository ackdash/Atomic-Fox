using System;
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
        private AnimationClip attackClip;
        public Action attackFinished;
        private GameObject attackProjectile;
        public GameObject AttackProjectilePrefab;

        public Action attackStarted;
        private bool isAttacking;
        private bool isProjectileActive;
        private LeftRightController leftRightController;

        public void Attack(InputValue btn)
        {
            if (btn.isPressed)
            {
                attackStarted?.Invoke();
                attackProjectile.SetActive(false);
                leftRightController.Stop();
                isAttacking = true;
            }
            else
            {
                attackFinished?.Invoke();
                isAttacking = false;
            }

            animator.SetBool(IsAttacking, isAttacking);
        }

        private void Awake()
        {
            leftRightController = GetComponent<LeftRightController>();
            animator = GetComponentsInChildren<Animator>()
                .First(r => r.CompareTag("CharacterGFX"));
        }

        private void Start()
        {
            attackProjectile = Instantiate(AttackProjectilePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            attackProjectile.SetActive(false);
        }

        private void Update()
        {
            if (isAttacking && !attackProjectile.activeSelf) FireProjectile();
        }

        private void FireProjectile()
        {
            // attackProjectile.transform.position = transform.position;
            // attackProjectile.SetActive(true);
        }
    }
}
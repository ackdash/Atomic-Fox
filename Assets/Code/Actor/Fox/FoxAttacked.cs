using System;
using System.Collections;
using Code.Interfaces;
using Code.Movement;
using UnityEngine;

namespace Code.Actor.Fox
{
    public class FoxAttacked : MonoBehaviour, IAttacked
    {
        private Direction lastAttackedFrom;

        [Header("Attack knock back Settings")] public float OnCollisionForceMultiplier;

        public float OnCollisionYDirectionFactor;
        public float OnStayForceMultiplier;
        public float OnStayForceYDirectionFactor;
        private Rigidbody2D rb;
        [SerializeField] private float autoCancelAttackInSeconds;

        public event Action<Direction> UnderAttack;
        public event Action AttackFinished;
        public bool IsUnderAttack { get; set; }

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void Attacked(Direction attackedFrom = Direction.Right)
        {
            IsUnderAttack = true;
            lastAttackedFrom = attackedFrom;
            UnderAttack?.Invoke(attackedFrom);
            rb.AddForce(new Vector2(attackedFrom.AsFloat(), OnCollisionYDirectionFactor) * OnCollisionForceMultiplier);
            StartCoroutine(FinishAttackRoutine());
        }

        public void AttackAndFinishImmediately()
        {
            Attacked();
            FinishAttack();
        }

        private IEnumerator  FinishAttackRoutine()
        { 
            yield return new WaitForSeconds(autoCancelAttackInSeconds);
            if (IsUnderAttack) FinishAttack();
        }
        
        public void FinishAttack()
        {
            IsUnderAttack = false;
            AttackFinished?.Invoke();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var isAttacker = other.gameObject.CompareTag("Enemy");

            if (isAttacker)
            {
                var impact = other.transform.position - transform.position;
                var attackedFrom = impact.x < 0f ? Direction.Right : Direction.Left;

                Attacked(attackedFrom);
            }
            else
            {
                FinishAttack();
            }
        }
    }
}
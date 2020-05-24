using System;
using Code.Interfaces;
using Code.Movement;
using UnityEngine;

namespace Code.Actor.Fox
{
    public class FoxAttacked : MonoBehaviour, IAttacked
    {
  
        [Header("Attack knock back Settings")]
        public float OnCollisionForceMultiplier;
        public float OnCollisionYDirectionFactor;
        public float OnStayForceMultiplier;
        public float OnStayForceYDirectionFactor;
        
        public event Action<Direction> UnderAttack;
        public event Action AttackFinished;
        private Rigidbody2D rb;
        private Direction lastAttackedFrom;
        public bool IsUnderAttack { get; set; }
        
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var isAttacker = other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player");
            
            if (isAttacker)
            {
                IsUnderAttack = true;
                var impact = other.transform.position - transform.position;
                var attackedFrom = impact.x < 0f ? Direction.Right : Direction.Left;
                lastAttackedFrom = attackedFrom;
                UnderAttack?.Invoke(attackedFrom);
                rb.AddForce(new Vector2(attackedFrom.AsFloat(), OnCollisionYDirectionFactor) * OnCollisionForceMultiplier);
            }
            else
            {
                IsUnderAttack = false;
                AttackFinished?.Invoke();
            }
        }

        // private void OnCollisionStay2D(Collision2D other)
        // {
        //     var isAttacker = other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player");
        //    
        //     if (!isAttacker || IsUnderAttack) return;
        //     
        //     IsUnderAttack = true;
        //     lastAttackedFrom = lastAttackedFrom == Direction.Left? Direction.Right : Direction.Left;
        //     UnderAttack?.Invoke(lastAttackedFrom);
        //     rb.AddForce(new Vector2(lastAttackedFrom.AsFloat(), OnStayForceYDirectionFactor) * OnStayForceMultiplier);
        // }


    }
}
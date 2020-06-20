using System;
using UnityEngine;

namespace Code.Actor
{
    public class InteractionEventProxy : MonoBehaviour
    {
        public Action<Collider2D> Interacted;
        public Action<Collision2D> CollisionInteracted;
        public Action<Collider2D> TriggerInteracted;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            Interacted?.Invoke(other.collider);
            CollisionInteracted?.Invoke(other);
        }

        private void OnTriggerEnter2D(Collider2D otherCollider)
        {
            Interacted?.Invoke(otherCollider);
            TriggerInteracted?.Invoke(otherCollider);
        }

    }
}
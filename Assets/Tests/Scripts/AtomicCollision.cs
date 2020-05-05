using Code.Framework.Events;
using UnityEngine;

namespace Tests.Scripts
{
    public class AtomicCollision : MonoBehaviour
    {
        [SerializeField] [InspectorName("Event")]
        private AtomicEvent collisionEvent;

        private void OnCollisionEnter2D (Collision2D other){
            collisionEvent.Trigger();
        }
        
        private void OnTriggerEnter2D(Collider2D other){
            collisionEvent.Trigger();
        }
    }
}
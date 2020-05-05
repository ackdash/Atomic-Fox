using Code.Events.Core;
using UnityEngine;

namespace Code.Events
{
    public class AtomicTriggerEnter2D : MonoBehaviour
    {
        [SerializeField] [InspectorName("Event")]
        private AtomicEvent collisionEvent;

        private void OnTriggerEnter2D(Collider2D other)
        {
            collisionEvent.Trigger();
        }
    }
}
using Code.Events.Core;
using UnityEngine;

namespace Code.Events
{
    public class AtomicCollisionEnter2D : MonoBehaviour
    {
        [SerializeField] [InspectorName("Event")]
        private AtomicEvent atomicEvent;

        private void OnCollisionEnter2D(Collision2D other)
        {
            atomicEvent.Trigger();
        }
    }
}
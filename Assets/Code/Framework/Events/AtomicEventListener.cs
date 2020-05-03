using UnityEngine;
using UnityEngine.Events;

namespace Code.Framework.Events
{
    public class AtomicEventListener : MonoBehaviour
    {
        [SerializeField] [InspectorName("AtomicEvent")]
        private AtomicEvent atomicEvent;

        [SerializeField] [InspectorName("Target")]
        private UnityEvent target;

        private void OnEnable()
        {
            atomicEvent.Register(this);
        }

        private void OnDisable()
        {
            atomicEvent.UnRegister(this);
        }

        public void OnEventTriggered()
        {
            target.Invoke();
        }
    }
}
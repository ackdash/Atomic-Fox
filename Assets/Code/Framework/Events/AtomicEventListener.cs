using UnityEngine;
using UnityEngine.Events;

namespace Code.Framework.Events
{
    public class AtomicEventListener : MonoBehaviour
    {
        [SerializeField] [InspectorName("AtomicEvent")]
        private AtomicEvent atomicEvent;

        private AtomicEventPipeline atomicEventPipeline;

        [SerializeField] [InspectorName("Target")]
        private UnityEvent target;

        private void Awake()
        {
            atomicEventPipeline = GetComponent<AtomicEventPipeline>();
        }

        private void OnEnable()
        {
            atomicEvent.Register(this);
        }

        private void OnDisable()
        {
            var hc = GetHashCode();
            atomicEvent.UnRegister(hc);
        }

        public void OnEventTriggered()
        {
            atomicEventPipeline.EnqueueAction(() => target.Invoke());
        }
    }
}
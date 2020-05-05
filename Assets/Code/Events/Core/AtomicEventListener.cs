using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Events.Core
{
    public class AtomicEventListener : MonoBehaviour
    {
        [SerializeField] [InspectorName("AtomicEvent")] [UsedImplicitly]
        private AtomicEvent atomicEvent;

        private AtomicEventPipeline atomicEventPipeline;

        [SerializeField] [InspectorName("Target")] [UsedImplicitly]
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
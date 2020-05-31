using UnityEngine;

namespace Code.Events.Core
{
    public class AtomicEventListenerBase: MonoBehaviour
    {
        protected AtomicEventPipeline atomicEventPipeline;
        public GameObject eventPipelineHost;
        
        private void Awake()
        {
            atomicEventPipeline = eventPipelineHost == null
                ? GetComponent<AtomicEventPipeline>()
                : eventPipelineHost.GetComponent<AtomicEventPipeline>();

            if (atomicEventPipeline == null && isActiveAndEnabled)
                throw new MissingReferenceException($"Atomic Pipeline missing {gameObject.name}");
            
        }
    }
}
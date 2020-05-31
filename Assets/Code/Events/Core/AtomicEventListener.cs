using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Events.Core
{
    public class AtomicEventListener : AtomicEventListenerBase
    {
        [SerializeField] [InspectorName("AtomicEvent")] [UsedImplicitly]
        protected AtomicEvent atomicEvent;
       
        [SerializeField] [InspectorName("Target")] [UsedImplicitly]
        protected UnityEvent target;

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
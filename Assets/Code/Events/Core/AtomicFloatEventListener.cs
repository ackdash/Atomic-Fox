using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Events.Core
{
    [Serializable]
    internal class UnityFloatEvent : UnityEvent<float>
    {
    }

    public class AtomicFloatEventListener : AtomicEventListenerBase
    {
        [SerializeField] [InspectorName("AtomicEvent")] [UsedImplicitly]
        private AtomicFloatEvent atomicFloatEvent;

        [SerializeField] [InspectorName("Target")] [UsedImplicitly]
        private UnityFloatEvent target;

        private void OnEnable()
        {
            atomicFloatEvent.Register(this);
        }

        private void OnDisable()
        {
            var hc = GetHashCode();
            atomicFloatEvent.UnRegister(hc);
        }

        public void OnEventTriggered(float floatValue)
        {
            atomicEventPipeline.EnqueueAction(() => target.Invoke(floatValue));
        }
    }
}
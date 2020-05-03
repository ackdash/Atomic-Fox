using System.Collections.Generic;
using UnityEngine;

namespace Code.Framework.Events
{
    [CreateAssetMenu]
    public class AtomicEvent : ScriptableObject
    {
        private readonly List<AtomicEventListener> listeners = new List<AtomicEventListener>();

        public void Register(AtomicEventListener listener)
        {
            listeners.Add(listener);
        }

        public void UnRegister<AtomicEventListener>(AtomicEventListener listener)
        {
            listeners.RemoveAll(a => a.GetHashCode() == listener.GetHashCode());
        }

        public void Trigger()
        {
            for (var i = 0; i < listeners.Count; i++) listeners[i].OnEventTriggered();
        }
    }
}
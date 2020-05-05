using System.Collections.Generic;
using UnityEngine;

namespace Code.Events.Core
{
    [CreateAssetMenu]
    public class AtomicEvent : ScriptableObject
    {
        private readonly Dictionary<int, AtomicEventListener> listeners = new Dictionary<int, AtomicEventListener>();

        public void Register(AtomicEventListener listener)
        {
            listeners.Add(listener.GetHashCode(), listener);
        }

        public void UnRegister(int hashCode)
        {
            listeners.Remove(hashCode);
        }

        public void Trigger()
        {
            foreach (var item in listeners)
            {
                item.Value.OnEventTriggered();
            }

            
        }
    }
}
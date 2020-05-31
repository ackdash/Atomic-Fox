using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Events.Core
{
    [CreateAssetMenu]
    [Serializable]
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

   
    [CreateAssetMenu]
    [Serializable]
    public class AtomicFloatEvent : ScriptableObject
    {
        private readonly Dictionary<int, AtomicFloatEventListener> listeners = new Dictionary<int, AtomicFloatEventListener>();

        public void Register(AtomicFloatEventListener listener)
        {
            listeners.Add(listener.GetHashCode(), listener);
        }

        public void UnRegister(int hashCode)
        {
            listeners.Remove(hashCode);
        }

        public void Trigger(float floatValue)
        {
            foreach (var item in listeners)
            {
                item.Value.OnEventTriggered(floatValue);
            }
        }
    }
}
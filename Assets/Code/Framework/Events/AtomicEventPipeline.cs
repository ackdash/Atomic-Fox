using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Framework.Events
{
    public class AtomicEventPipeline : MonoBehaviour
    {
        private readonly Queue<Action> queue = new Queue<Action>();

        private void Update()
        {
            if (queue.Count > 0) FlushQueue();
        }

        private void FlushQueue()
        {
            while (queue.Count > 0) queue.Dequeue()();
        }

        public void EnqueueAction(Action target)
        {
            queue.Enqueue(target);
        }
    }
}
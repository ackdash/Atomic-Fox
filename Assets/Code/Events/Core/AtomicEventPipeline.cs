using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Events.Core
{

    public class AtomicEventPipeline : MonoBehaviour
    {

        private readonly Queue<Action> queue = new Queue<Action>();

        /// <summary>
        /// Naive implementation of an event pipeline for discreet
        /// events. Ran out of time to implement within the game jam.   
        /// </summary>
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
using System;
using System.Collections.Generic;
using Code.Interfaces.Game;
using UnityEngine;
using UnityEngine.Animations;

namespace Code.ItemCollection
{
    public class ItemCollectable : MonoBehaviour, ICollectable, IDropable, IResetable
    {
        private readonly List<ConstraintSource> sources = new List<ConstraintSource>();
        private Collider2D col;

        private ParentConstraint pc;
        public bool IsCollected { get; private set; }

        public void Collect(Transform collector)
        {
            var sc = new ConstraintSource {sourceTransform = collector, weight = 1f};
            pc.AddSource(sc);
            col.enabled = false;
            IsCollected = true;
            Collected?.Invoke();
        }

        public void Drop()
        {
            RemoveAllSources();
            col.enabled = true;
            IsCollected = false;
            Dropped?.Invoke();
        }

        public void Reset()
        {
            RemoveAllSources();
            col.enabled = true;
            IsCollected = false;
            StateReset?.Invoke();
        }

        public event Action Collected;
        public event Action Dropped;
        public event Action StateReset;

        private void Start()
        {
            pc = GetComponent<ParentConstraint>();
            col = GetComponent<Collider2D>();
        }

        private void RemoveAllSources()
        {
            pc.GetSources(sources);
            for (var i = 0; i < sources.Count; i++) pc.RemoveSource(i);
        }
    }
}
using System;
using System.Collections.Generic;
using Code.Interfaces.Game;
using UnityEngine;
using UnityEngine.Animations;

namespace Code.ItemCollection
{
    public class ItemCollector : MonoBehaviour, ICollector
    {
        private readonly List<Transform> inventory = new List<Transform>();
        public bool HasItems { get; private set; }

        public void Collect(GameObject item)
        {
            var collectable = item.GetComponent<ICollectable>();
            collectable?.Collect(transform);
        }

        public event Action ItemCollected;

        public event Action ItemDropped;


        private void OnTriggerEnter2D(Collider2D other) => Collect(other);

        private void OnCollisionEnter2D(Collision2D other) => Collect(other.collider);

        public void DropItems()
        {
            inventory.ForEach(a =>
            {
                var dropableItems = a.GetComponent<IDropable>();
                dropableItems?.Drop();
            });
            inventory.Clear();
        }

        public void Collect(Behaviour other)
        {
            var collectableItem = other.gameObject.GetComponent<ICollectable>();
            var collector = other.gameObject.GetComponent<ICollector>();

            if (other.CompareTag("Ship"))
            {
                inventory.ForEach(a =>
                {
                    var item = a.gameObject.GetComponent<IResetable>();
                    item?.Reset();
                });
                inventory.Clear();
                ItemDropped?.Invoke();
                HasItems = false;
            }

            else if (collectableItem != null)
            {
                HasItems = true;
                inventory.Add(other.transform);
                collectableItem.Collect(transform);
                ItemCollected?.Invoke();
            }
            // else 
            
            // if (collector != null)
            // {
            //     inventory.ForEach(a =>
            //     {
            //         var _pc = a.GetComponent<ParentConstraint>();
            //         _pc.RemoveSource(0);
            //         a.position = a.parent.position;
            //     });
            //     inventory.Clear();
            //     HasItems = false;
            // }
        }
    }
}
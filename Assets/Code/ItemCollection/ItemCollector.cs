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
        public bool HasItems { get; set; }

        public Transform GetItem(string itemTag)
        {
            foreach (var item in inventory)
            {
                if (item.CompareTag(itemTag))
                {
                    return item;
                }
            }

            return null;

        }

        public bool CanCollect { get; set; }

        public void Clear()
        {
            inventory.Clear();
            HasItems = false;
        }

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
            if (!CanCollect) return;
            
            var collectableItem = other.gameObject.GetComponent<ICollectable>();
            var collector = other.gameObject.GetComponent<ICollector>();

           if (collectableItem != null)
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
            //         
            //         a.position = a.parent.position;
            //     });
            //     inventory.Clear();
            //     HasItems = false;
            // }
        }
    }
}
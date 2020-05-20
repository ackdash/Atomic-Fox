using System.Collections.Generic;
using Code.Interfaces.Game;
using UnityEngine;
using UnityEngine.Animations;

namespace Code.ItemCollection
{
    public class ItemCollector : MonoBehaviour, ICollector
    {
        private List<Transform> CollectedItems = new List<Transform>();

        public void Collect(GameObject item)
        {
            Debug.Log($"Picked up {item.tag}");
        }

        // Start is called before the first frame update
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            var collectable = other.gameObject.GetComponent<ICollectable>();
            var collector = other.gameObject.GetComponent<ICollector>();

            if (other.CompareTag("Ship"))
            {
                CollectedItems.ForEach(a =>
                {
                    var _pc = a.GetComponent<ParentConstraint>();
                    _pc.RemoveSource(0);
                    a.position = a.parent.position;
                    var col = a.GetComponent<Collider2D>();
                    col.enabled = true;
                });
                CollectedItems = new List<Transform>();
            }
           
            else if (collectable != null)
            {
                other.enabled = false;
                var pc = other.gameObject.GetComponent<ParentConstraint>();
                var sc = new ConstraintSource {sourceTransform = transform, weight = 1f};

                pc.AddSource(sc);
                CollectedItems.Add(other.transform);
                
            } else if (collector != null)
            {
                CollectedItems.ForEach(a =>
                {
                    var _pc = a.GetComponent<ParentConstraint>();
                    _pc.RemoveSource(0);
                    a.position = a.parent.position;
                    var sc = new ConstraintSource {sourceTransform = transform, weight = 1f};
                    var col = a.GetComponent<Collider2D>();
                    _pc.AddSource(sc);
                    col.enabled = true;
                });

                CollectedItems = new List<Transform>();
            }
        }
    }
}
using System.Collections.Generic;
using Code.Interfaces.Game;
using UnityEngine;
using UnityEngine.Animations;

namespace Code.ItemCollection
{
    public class ItemCollector : MonoBehaviour, ICollector
    {
        private List<GameObject> CollectedItems = new List<GameObject>();

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
            // Debug.Log($"OnTriggerEnter2D up {other.tag}");
            if (collector != null)
            {
                // Debug.Log($"SDSDSDSDSDS {other.tag}");
                CollectedItems.ForEach((a) =>
                {
                    var _pc = a.GetComponent<ParentConstraint>();
                    _pc.RemoveSource(0);
                    a.transform.position = a.transform.parent.position;
                    var col = a.GetComponent<Collider2D>();
                    col.enabled = true;
                });


                CollectedItems = new List<GameObject>();
                // other.enabled = false;
                // var pc = other.gameObject.GetComponent<ParentConstraint>();
                // var sc = new ConstraintSource {sourceTransform = transform, weight = 1};
                // pc.AddSource(sc);
            }
            else if (collectable != null)
            {
                other.enabled = false;
                var pc = other.gameObject.GetComponent<ParentConstraint>();
                var sc = new ConstraintSource {sourceTransform = transform, weight = 0f};
                pc.AddSource(sc);
                CollectedItems.Add(other.gameObject);
            }
        }
    }
}
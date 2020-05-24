using System;
using Code.ItemCollection;
using Code.Movement;
using UnityEngine;
using UnityEngine.Animations;

namespace Code.Item
{
    public class FuelController : MonoBehaviour
    {
        private FallChecker fallChecker;
        public GameObject splatParent;
        public GameObject splatPrefab;
        public ParentConstraint parentConstraint;
        
        [SerializeField] public bool isCollected;

        private ItemCollectable itemCollectable;
        private Rigidbody2D rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            fallChecker = GetComponent<FallChecker>();
            itemCollectable = GetComponent<ItemCollectable>();
            
            rb.gravityScale = 0;

            itemCollectable.Collected += () => Collected();
            itemCollectable.Dropped += () => Dropped();
            itemCollectable.StateReset += () => Reset();
        }

        private void Update()
        {
            if (itemCollectable.IsCollected) return;
            fallChecker.Calculate();
        }

        private void FixedUpdate()
        {
            if (!fallChecker.IsFalling) return;
            rb.AddForce(Vector2.left * (50f *Math.Sign( Mathf.Sin(Time.frameCount))));
        }

        private void Collected()
        {
            rb.gravityScale = 0f;
            PreventMovement();
        }

        private void Dropped()
        {
            AllowMovement();
            rb.gravityScale = 0.25f;
        }

        private void Reset()
        {
            if (parentConstraint) parentConstraint.RemoveSource(0);
            rb.gravityScale = 0f;
            transform.position = transform.parent.position;
            PreventMovement();
        }

        private void PreventMovement()
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        private void AllowMovement()
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }
       
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!itemCollectable.IsCollected
                && splatParent != null
                && other.gameObject.CompareTag("Level Tiles"))
            {
                var position = transform.position;
                var impact = other.transform.position - position;
                var xImpact = Mathf.Abs(position.x) - Mathf.Abs(impact.x);
                var impactUnderneath = (xImpact> -0.5f && xImpact < 0.5f);

                if (!impactUnderneath) return;

                var splat =Instantiate(splatPrefab, transform);
                splat.transform.parent = splatParent.transform;
                PreventMovement();
                itemCollectable.Reset();
            }
        }
    }
}
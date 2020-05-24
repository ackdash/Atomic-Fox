using System;
using Code.ItemCollection;
using UnityEngine;

namespace Code.Movement
{
    public class FuelController : MonoBehaviour
    {
        private FallChecker fallChecker;
        public GameObject SplatParent;

        [SerializeField] public bool IsCollected;

        private ItemCollectable itemCollectable;
        private Rigidbody2D rb;

        public GameObject splatPrefab;
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
            rb.gravityScale = 0f;
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
                && SplatParent != null
                && other.gameObject.CompareTag("Level Tiles"))
            {
                // TODO: Simplify this
                var impact = other.transform.position - transform.position;
                var xImpact = Mathf.Abs(transform.position.x) - Mathf.Abs(impact.x);
                var impactUnderneath = (xImpact> -0.5f && xImpact < 0.5f);

                if (!impactUnderneath) return;

                var splat =Instantiate(splatPrefab, transform);
                splat.transform.parent = SplatParent.transform;
                PreventMovement();
                itemCollectable.Reset();
            }
        }
    }
}
using System;
using Code.ItemCollection;
using UnityEngine;

namespace Code.Movement
{
    public class FuelController : MonoBehaviour
    {
        private FallChecker fallChecker;

        [SerializeField] public bool IsCollected;

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
            StopMovement();
        }

        private void Dropped()
        {
            rb.gravityScale = 0.25f;
            StopMovement();
        }

        private void Reset()
        {
            rb.gravityScale = 0f;
            StopMovement();
        }

        private void StopMovement()
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }
}
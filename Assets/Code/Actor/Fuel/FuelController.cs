using System;
using Code.Interfaces.Game;
using Code.ItemCollection;
using Code.Movement;
using UnityEngine;
using UnityEngine.Animations;

namespace Code.Actor.Fuel
{
    public class FuelController : MonoBehaviour, IResetable
    {
        private FallChecker fallChecker;
        private bool gameOver;

        [SerializeField] public bool isCollected;
        [SerializeField] public bool canRespawn;

        private ItemCollectable itemCollectable;
        public ParentConstraint parentConstraint;
        private Rigidbody2D rb;

        private GroundChecker side1Check;
        private GroundChecker side2Check;
        
        public GameObject splatParent;
        public GameObject splatPrefab;

        private Vector2 spawnLocation;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            fallChecker = GetComponent<FallChecker>();
            itemCollectable = GetComponent<ItemCollectable>();
            parentConstraint = GetComponent<ParentConstraint>();
        
            var groundCheckers = GetComponentsInChildren<GroundChecker>();
            side1Check = groundCheckers[0];
            side2Check = groundCheckers[1];

            rb.gravityScale = 0;
            spawnLocation = transform.position;
                
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
            if (!gameOver && !fallChecker.IsFalling || !gameOver) return;
            rb.AddForce(Vector2.left * (50f * Math.Sign(Mathf.Sin(Time.frameCount))));
        }

        public void GameOver()
        {
            gameOver = true;
            Dropped();
        }

        private void Collected()
        {
            rb.gravityScale = 0f;
            PreventMovement();
        }

        public void Dropped()
        {
            AllowMovement();
            rb.gravityScale = gameOver ? -5f : 0.25f;
        }

        public void Reset()
        {
            if (canRespawn) Respawn();
            else gameObject.SetActive(false);
        }

        private void Respawn()
        {
            side1Check.Reset();
            side2Check.Reset();
            rb.gravityScale = 0f;
            transform.position = spawnLocation;
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
            var sc1r = side1Check.Check();
            var sc2r = side2Check.Check();
            var comparedTag = other.gameObject.CompareTag("Level Tiles");

            if (!itemCollectable.IsCollected
                && splatParent != null
                && other.gameObject.CompareTag("Level Tiles")
                && side1Check.Check() && side2Check.Check())
            {
                var splat = Instantiate(splatPrefab, transform);
                splat.transform.parent = splatParent.transform;

                if (gameOver) return;

                PreventMovement();
                itemCollectable.Reset();
            }
        }
    }
}
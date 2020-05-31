using Code.Events.Core;
using Code.Interfaces.Game;
using UnityEngine;

namespace Code.Actor.Rocket
{
    public class RocketController : MonoBehaviour
    {
        [SerializeField] private float launchSpeed;

        public GameObject rocketOwner;
        private int rocketOwnerId;
        private Vector2 startingPosition;
        private bool takeOff;
        private bool canTakeOff;
        private RocketFuelTankController tank;
        public AtomicEvent RocketTakingOff;

        public void Start()
        {
            startingPosition = transform.position;
            rocketOwnerId = rocketOwner.GetInstanceID();
            tank = GetComponentInChildren<RocketFuelTankController>();
        }

        public void Reset()
        {
            takeOff = false;
            canTakeOff = false;
            transform.position = startingPosition;
        }

        public void CanTakeOff()
        {
            canTakeOff = true;
        }
        
        public void TakeOff()
        {
            if (!canTakeOff) return;
            
            takeOff = true;
            RocketTakingOff.Trigger();
        }

        private void Update()
        {
            if (takeOff) transform.Translate(Vector2.up * launchSpeed);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            var otherCollector = other.GetComponent<ICollector>();
            var isOwner = other.gameObject.GetInstanceID() != rocketOwnerId;

            if (isOwner && otherCollector != null && otherCollector.HasItems) tank.ShowReceptor();
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.GetInstanceID() != rocketOwnerId) return;
            tank.HideReceptor();
        }
    }
}
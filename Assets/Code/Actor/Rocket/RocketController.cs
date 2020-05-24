using Code.Interfaces.Game;
using UnityEngine;

namespace Code.Actor.Rocket
{
    public class RocketController : MonoBehaviour
    {
        public GameObject rocketOwner;
        private int rocketOwnerId;
        private RocketFuelTankController tank;

        // TODO: Get Parent from GameObject hierarchy
        public void Start()
        {
            rocketOwnerId = rocketOwner.GetInstanceID();
            tank = GetComponentInChildren<RocketFuelTankController>();
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            var otherCollector = other.GetComponent<ICollector>();
            var isOwner= other.gameObject.GetInstanceID() != rocketOwnerId;
            
            if (isOwner && otherCollector != null && otherCollector.HasItems ) tank.ShowReceptor();
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.GetInstanceID() != rocketOwnerId) return;
            tank.HideReceptor();
        }
    }
}
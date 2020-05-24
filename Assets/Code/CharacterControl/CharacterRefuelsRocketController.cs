using Code.Events.Core;
using Code.Interfaces.Game;
using Data;
using UnityEngine;

namespace Code.CharacterControl
{
    public class CharacterRefuelsRocketController : MonoBehaviour
    {
        public ICollector collector;
        public AtomicEvent fuelCollectedEvent;
        public GameObject rocket;
        private int rocketId;

        public void Start()
        {
            rocketId = rocket.GetInstanceID();
            collector = GetComponent<ICollector>();
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!collector.HasItems || other.gameObject.GetInstanceID() != rocketId) return;

            var fuel = collector.GetItem("Fuel");

            if (fuel == null) return;

            var item = fuel.gameObject.GetComponent<IResetable>();

            if (item == null || !other.CompareTag("Fuel Receptor")) return;
            
            fuelCollectedEvent.Trigger();

            item.Reset();
            collector.Clear();
        }
    }
}
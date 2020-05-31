using Code.Events.Core;
using Code.Interfaces.Game;
using UnityEngine;

namespace Code.Actor.Rocket
{
    public class CharacterRefuelsRocketController : MonoBehaviour
    {
        public ICollector collector;
        public AtomicEvent fuelCollectedEvent;
        public GameObject fuelReceptor;
        private int fuelReceptorId;

        public void Start()
        {
            fuelReceptorId = fuelReceptor.GetInstanceID();
            collector = GetComponent<ICollector>();
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!collector.HasItems || other.gameObject.GetInstanceID() != fuelReceptorId) return;

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
using Code.Events.Core;
using Code.Interfaces.Game;
using UnityEngine;

namespace Code.Actor.Rocket
{
    public class CharacterRefuelsRocketController : MonoBehaviour
    {
        [SerializeField] private bool canFuel;
        public ICollector collector;
        public AtomicEvent fuelCollectedEvent;
        public GameObject fuelReceptor;
        private int fuelReceptorId;

        public void Start()
        {
            fuelReceptorId = fuelReceptor.GetInstanceID();
            collector = GetComponent<ICollector>();
            canFuel = true;
        }

        public void EnableFueling() => canFuel = true;

        public void DisableFueling() => canFuel = false;

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!canFuel || !collector.HasItems || other.gameObject.GetInstanceID() != fuelReceptorId) return;

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
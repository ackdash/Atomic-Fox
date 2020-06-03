using Code.Actor.Fuel;
using Code.Interfaces.Game;
using UnityEngine;

namespace Code.Actor.BloomingFlower
{
    public class BloomingFlowerController : MonoBehaviour, IResetable
    {
        private FuelController[] bloomingFlowerFuel;
  
        void Start()
        {
            bloomingFlowerFuel = GetComponentsInChildren<FuelController>();
        }
    
        public void Reset()
        {
            foreach (var fuel in bloomingFlowerFuel) fuel.Respawn();
        }
    }
}

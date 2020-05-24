using System;
using Code.Interfaces.Game;
using UnityEngine;
using UnityEngine.Animations;

namespace Code.CharacterControl
{
    public class RocketRefuel : MonoBehaviour
    {
        public GameObject rocket;
        public int rocketId;
        public ICollector collector;

        public void Start()
        {
            rocketId = rocket.GetInstanceID();
            collector = GetComponent<ICollector>();
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!collector.HasItems || other.gameObject.GetInstanceID() != rocketId) return;
            
            var fuel = collector.GetItem("Fuel");
            var _pc = fuel.GetComponent<ParentConstraint>();
            var item = fuel.gameObject.GetComponent<IResetable>();
               
            var source = _pc.GetSource(0);
            _pc.RemoveSource(0);
            item?.Reset();
            fuel.position = fuel.parent.position;
            collector.Clear();
            var fuelTank = other.GetComponent<RocketFuelTank>();
            fuelTank.AddFuel(1f);

            Debug.Log("Bingo!!!");

        }
    }
}
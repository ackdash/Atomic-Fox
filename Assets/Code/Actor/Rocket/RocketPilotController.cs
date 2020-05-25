using System;
using Code.Events.Core;
using UnityEngine;

namespace Code.Actor.Rocket
{
    public class RocketPilotController:MonoBehaviour
    {
        [SerializeField] public AtomicEvent CharacterIsPiloting;
        [SerializeField] public AtomicEvent CharacterIsNotPiloting;
        private void OnTriggerEnter2D(Collider2D other)
        {
            CharacterIsPiloting.Trigger();
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            CharacterIsNotPiloting.Trigger();
        }
    }
}
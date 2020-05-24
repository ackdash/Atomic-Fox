using Code.Data;
using Code.Events.Core;
using Code.Interfaces;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Playables;

namespace Code.Actor.Rocket
{
    public class RocketWinState : MonoBehaviour, ICompletor
    {
        public IntReference targetFuelLevel;
        public IntReference currentFuelLevel;
        public AtomicEvent characterCompletedLevelEvent;

        public bool TargetReached() => currentFuelLevel.Value >= targetFuelLevel.Value;
        
        public bool CheckState()
        {
            var levelComplete = TargetReached();
            Debug.Log($"Level is {levelComplete.ToString()}");
            
            if (levelComplete) characterCompletedLevelEvent.Trigger();

            return levelComplete;
        }
        
        public void CheckIfComplete()
        {
            CheckState();
        }
    }
}

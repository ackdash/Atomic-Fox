using Code.Events.Core;
using UnityEngine;

namespace Code.Actor.Rocket
{
    public class RocketGangwayController : MonoBehaviour
    {
        private readonly Quaternion rotationAtLowered = Quaternion.Euler(0f, 0f, 0f);
        private readonly Quaternion rotationAtRaised = Quaternion.Euler(0f, 0f, 90f);

        private State currentState = State.Lowered;
        
        public AtomicEvent Lowered;
        public AtomicEvent Raised;
        public AtomicEvent Locked;
        public AtomicEvent Unlocked;

        private bool haveLoweredEvent;
        private bool haveRaisedEvent;
        private bool haveLockedEvent;
        private bool haveUnlockedEvent;

        private bool isLocked;
        private bool lockWhenNextClosed;
        
        public void Start()
        {
            haveRaisedEvent = Raised != null;
            haveLoweredEvent = Lowered != null;
            haveLockedEvent = Locked != null;
            haveUnlockedEvent = Unlocked != null;
        }

        private void Update()
        {
            if (isLocked) return;
            
            if (!isLocked && 
                (currentState == State.Lowered || currentState == State.Raised)) return;

            var zVal = currentState == State.Raising ? 90f : -90f;

            transform.rotation *= Quaternion.Euler(0f, 0f, zVal * Time.deltaTime);

            if (currentState == State.Raising && transform.rotation.eulerAngles.z > 90f) RaisedGangway();
            if (currentState == State.Lowering && transform.rotation.z < 0.01f) LoweredGangway();
        }

        public void Lock()
        {
            if (currentState == State.Raised)
            {
                if (haveLockedEvent) Locked.Trigger();
                lockWhenNextClosed = false;
                isLocked = true;
            }
            else lockWhenNextClosed = true;
        }

        public void UnLock()
        {
            isLocked = false;
            lockWhenNextClosed = false;
            if (haveUnlockedEvent) Unlocked.Trigger();
        }
        
        public void RaiseGangway() => currentState = State.Raising;

        private void RaisedGangway()
        {
            transform.rotation = rotationAtRaised;
            currentState = State.Raised;
            
            if (haveRaisedEvent) Raised.Trigger();
            
            if (lockWhenNextClosed) Lock();
        }

        public void LowerGangway() => currentState = State.Lowering;

        private void LoweredGangway()
        {
            transform.rotation = rotationAtLowered;
            currentState = State.Lowered;

            if (haveLoweredEvent) Lowered.Trigger();
        }
        
        private enum State
        {
            Raising,
            Raised,
            Lowering,
            Lowered
        }

    }
}
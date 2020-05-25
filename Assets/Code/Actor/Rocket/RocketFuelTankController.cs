using Code.Data;
using Code.Events.Core;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Code.Actor.Rocket
{
    public class RocketFuelTankController : MonoBehaviour
    {
        private Vector3 activePosition;
        [SerializeField] private int capacity;
        [SerializeField] private IntReference amountOfFuel;

        [SerializeField] private FloatReference animationSpeed;
        private Collider2D collider;

        [SerializeField] private FloatReference distanceToProtude;

        private Vector3 inactivePosition;

        private bool isAnimationRunning;

        private bool isReceptorShowing;

        private Light2D[] lights;

        private bool receptorIsActive;
        
        [SerializeField]
        private AtomicEvent fuelTankFullEvent;
        private bool fuelTankFullEventIsNull;
        
        private void Awake()
        {
            lights = GetComponentsInChildren<Light2D>();
            collider = GetComponent<Collider2D>();
            fuelTankFullEventIsNull = fuelTankFullEvent == null;
        }

        private void Start()
        {
            collider.enabled = false;
            amountOfFuel.Value = 0;
            inactivePosition = transform.position;
            activePosition = new Vector2(inactivePosition.x + distanceToProtude.Value, inactivePosition.y);
            TurnOffLights();
        }

        private void Update()
        {
            var step = animationSpeed.Value * Time.deltaTime;
            isReceptorShowing = transform.position == activePosition;

            if (!isReceptorShowing && receptorIsActive)
                transform.position = Vector2.MoveTowards(transform.position, activePosition, step);
            else if (!receptorIsActive)
                transform.position = Vector2.MoveTowards(transform.position, inactivePosition, step);
        }

        public void AddFuel(int amount)
        {
            amountOfFuel.Value += amount;
            if (amountOfFuel.Value >= capacity && !fuelTankFullEventIsNull)
            {
                fuelTankFullEvent.Trigger();
            }
        }

        public void ShowReceptor()
        {
            receptorIsActive = true;
            TurnOnLights();
            collider.enabled = receptorIsActive;
        }

        private void TurnOffLights() => SwitchLights(LightState.Off);
        private void TurnOnLights() => SwitchLights(LightState.On);

        private void SwitchLights(LightState lightState)
        {
            foreach (var light2D in lights) light2D.enabled = lightState == LightState.On;
        }

        public void HideReceptor()
        {
            receptorIsActive = false;
            collider.enabled = receptorIsActive;
            TurnOffLights();
        }

        private enum LightState
        {
            Off,
            On
        }
    }
}
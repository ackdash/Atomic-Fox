using Code.Data;
using Data;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Code.Actor.Rocket
{
    public class RocketFuelTankController : MonoBehaviour
    {
        private Vector3 activePosition;

        [SerializeField] private FloatReference amountOfFuel;

        [SerializeField] private FloatReference animationSpeed;
        private Collider2D collider;

        [SerializeField] private FloatReference distanceToProtude;

        private Vector3 inactivePosition;

        private bool isAnimationRunning;

        private bool isReceptorShowing;

        private Light2D[] lights;

        private bool receptorIsActive;

        private void Awake()
        {
            lights = GetComponentsInChildren<Light2D>();
            collider = GetComponent<Collider2D>();
        }

        private void Start()
        {
            collider.enabled = false;
            amountOfFuel.Value = 0f;
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

        public void AddFuel(float amount)
        {
            amountOfFuel.Value += amount;
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
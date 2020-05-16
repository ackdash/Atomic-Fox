using Code.Interfaces;
using UnityEngine;

namespace Code.Movement
{
    /// <summary>
    ///     Controls the movement speed of an object
    ///     Note: It's the responsibility of the movement controllers to decide
    ///     if an object should be moving or not. If the object does move, then it
    ///     should use the speed access via the property `CurrentSpeed`.
    /// </summary>
    public class SpeedController : MonoBehaviour, ICurrentSpeed
    {
        private readonly AnimationCurve curve = AnimationCurve.EaseInOut(0.0f, 0.0f, 2.0f, 2.0f);
        private float defaultSpeed;
        private bool isAccelerating;

        [SerializeField] [InspectorName("SpeedShift")] [Range(0f, 1f)]
        private float speedShiftFactor = 0.05f;


        private float t;
        private float targetSpeed;
        public float CurrentSpeedNormalised { get; private set; }
        public float CurrentSpeed { get; private set; }

        private void Start()
        {
            CurrentSpeed = defaultSpeed;
            targetSpeed = defaultSpeed;
            CurrentSpeedNormalised = 1f;
        }

        private void Update()
        {
            if (isAccelerating)
            {
                if (!Mathf.Approximately(targetSpeed, CurrentSpeed))
                {
                    t += Time.deltaTime * 0.75f;
                    CurrentSpeed = Mathf.Lerp(CurrentSpeed, targetSpeed, curve.Evaluate(t));
                    CurrentSpeedNormalised = CurrentSpeed / defaultSpeed;
                }
                else
                {
                    isAccelerating = false;
                }
            }
        }

        public void SetDefaultSpeed(float speed)
        {
            defaultSpeed = speed;
        }
        
        public void OnResetSpeed()
        {
            ResetSpeed();
        }

        public void OnDecreaseSpeed()
        {
            CurrentSpeed *= speedShiftFactor;
        }

        public void OnIncreaseSpeed()
        {
            CurrentSpeed *= 1 / speedShiftFactor;
        }

        private void Accelerate(float factor)
        {
            t = 0;
            isAccelerating = true;
            targetSpeed = CurrentSpeed * speedShiftFactor;
        }

        private void ResetSpeed()
        {
            t = 0;
            isAccelerating = false;
            CurrentSpeed = defaultSpeed;
        }
    }
}
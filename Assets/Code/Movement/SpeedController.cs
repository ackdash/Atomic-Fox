using UnityEngine;

namespace Code.Movement
{
    public class SpeedController : MonoBehaviour
    {
        private readonly AnimationCurve curve = AnimationCurve.EaseInOut(0.0f, 0.0f, 2.0f, 2.0f);
        
        private float defaultSpeed = 1;

        private bool isAccelerating;


        [SerializeField] [InspectorName("SpeedShift")] [Range(0f, 1f)]
        private float speedShiftFactor = 0.05f;

        private float t;
        private float targetSpeed;

        public float CurrentSpeed { get; private set; }

        private void Awake()
        {
            CurrentSpeed = defaultSpeed;
            targetSpeed = defaultSpeed;
        }

        private void Update()
        {
            if (isAccelerating)
            {
                if (!Mathf.Approximately(targetSpeed, CurrentSpeed))
                {
                    t += Time.deltaTime * 0.75f;
                    CurrentSpeed = Mathf.Lerp(CurrentSpeed, targetSpeed, curve.Evaluate(t));
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

        public void OnSlowDown()
        {
            Accelerate(0.05f);
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
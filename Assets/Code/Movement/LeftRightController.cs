using Code.Interfaces;
using UnityEngine;

namespace Code.Movement
{
    public class LeftRightController : MonoBehaviour, ICurrentSpeed
    {
        public Direction direction;

        [SerializeField] private float speed;

        private SpeedController speedController;

        public float HorizontalSpeed { get; private set; }

        public float CurrentSpeed => speedController.CurrentSpeed * direction.AsFloat();

        private void Awake()
        {
            speedController = GetComponent<SpeedController>();

            if (speedController != null) speedController.SetDefaultSpeed(speed);
        }

        public float Calculate()
        {
            if (direction == Direction.None) return 0;

            var scaledDirection = -direction.AsFloat() * Time.deltaTime;
            var desiredSpeed = speedController ? speedController.CurrentSpeed : speed;
           
            return  scaledDirection * desiredSpeed;
        }

        public void HeadLeft() => SetHeading(Direction.Left);

        public void HeadRight() => SetHeading(Direction.Right);

        private void SetHeading(Direction heading)
        {
            var t = transform;
            var ls = t.localScale;
            direction = heading;
            t.localScale = new Vector3(-direction.AsFloat(), ls.y, ls.z);
            HorizontalSpeed = 1f;
        }

        public void Turn()
        {
            if (direction == Direction.Left) HeadRight();
            else HeadLeft();
        }

        public void Stop()
        {
            direction = Direction.None;
            HorizontalSpeed = 0f;
        }
    }
}
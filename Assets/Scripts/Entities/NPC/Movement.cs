using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class Movement : MonoBehaviour
    {
        public Vector3 TowardsPosition;
        public GameObject Target;

        public float MovementAmp = 1f;
        public float Speed;

        private const int _rotationSpeed = 65;
        private AnimationHandler _animHandler;

        private readonly int xAngleCutOff = 15;

        public void Start()
        {
            _animHandler = GetComponent<AnimationHandler>();
            TowardsPosition = transform.position;
        }

        private float _angle;
        public void Update()
        {
            if (Target == null)
            {
                this.GetComponent<BehaviorExecutor>().enabled = false;
                this.enabled = false;
                return;
            }
            MovementAmp = Vector3.Distance(transform.position, Target.transform.position) < 6 ? 0.3f : 1f;
           _animHandler.SetAnimationSpeed(MovementAmp);

            Speed = MovementAmp * 5.5f;


            _angle = Vector3.Angle(TowardsPosition - transform.position, transform.forward);

            MoveModel();
            RotateModel();
            AnimateModel();
        }

        private void MoveModel()
        {
            if (_angle > xAngleCutOff && _angle < 65)
                return;

            var newPos = Vector3.MoveTowards(transform.position, TowardsPosition, Time.deltaTime * Speed);
            transform.position = new Vector3(newPos.x, transform.position.y, newPos.z);
        }

        private void AnimateModel()
        {
            int x = _angle > 65 && _angle < 105 ? 1 : 0;
            int y = _angle < xAngleCutOff ? 1 : _angle > 175 ? -1 : 0;

            //Check whether the x position of TowardsPosition is left or right of the AI
            var i = transform.InverseTransformPoint(TowardsPosition);
            x *= i.x < 0 ? -1 : 1;

            if (Vector3.Distance(transform.position, TowardsPosition) < 1f)
                y = 0;

            _animHandler.SetMovement(x, y);
        }

        private void RotateModel()
        {
            var pos = new Vector3(transform.position.x, 0, transform.position.z);
            var targetPos = new Vector3(Target.transform.position.x, 0, Target.transform.position.z);

            var newDir = Vector3.RotateTowards(transform.forward, targetPos - pos, Time.deltaTime * _rotationSpeed * Mathf.Deg2Rad, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }

        private Vector3 GetAnglePoint(int degrees)
        {
            float offsetDegrees = (transform.rotation.y + degrees) * Mathf.Deg2Rad;
            return new Vector3(Mathf.Sin(offsetDegrees), 0, Mathf.Cos(offsetDegrees)) * 3;
        }
    }
}

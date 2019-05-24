using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;

namespace Assets.Scripts.Behaviour.Actions.Movement
{
    [Action("Gladiator/Movement/CircleAroundTarget")]
    [Help("Circles around the target")]
    public class CircleAroundTarget : GOAction
    {
        [InParam("Target")]
        [Help("Target to check the distance")]
        public GameObject Target;

        private Entities.Movement _movement;

        private float _angle;
        private float _distanceToTarget;

        private Transform _trans;
        private Transform _tTrans;

        public override void OnStart()
        {
            _movement = gameObject.GetComponent<Entities.Movement>();

            _trans = gameObject.transform;
            _tTrans = Target.transform;
                       
            _distanceToTarget = Vector3.Distance(_trans.position, _tTrans.position) + 0.3f;
        }

        public override TaskStatus OnUpdate()
        {
            _angle = GetAngle();
            const float offsetDegrees = 25 * Mathf.Deg2Rad;
            var offset = new Vector3(Mathf.Sin(_angle - offsetDegrees), 0, Mathf.Cos(_angle - offsetDegrees)) * _distanceToTarget;
            var newPos = _tTrans.position + offset;

            _movement.TowardsPosition = newPos;

            return TaskStatus.COMPLETED;
        }

        private float GetAngle()
        {
            Vector3 difference = _trans.position - _tTrans.position;
            float angle = Mathf.Atan2(difference.x, _trans.position.z);
            return angle;
        }
    }
}

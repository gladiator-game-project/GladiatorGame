using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;

namespace Assets.Scripts.Behaviour.Actions
{
    [Action("Gladiator/Movement/CircleAroundTarget")]
    [Help("Circles around the target")]
    public class CircleAroundTarget : GOAction
    {
        [InParam("Target")]
        [Help("Target to check the distance")]
        public GameObject Target;

        private Movement _movement;

        private float _angle = 0;
        private float _distanceToTarget;

        private Transform trans;
        private Transform tTrans;

        public override void OnStart()
        {
            _movement = gameObject.GetComponent<Movement>();

            trans = gameObject.transform;
            tTrans = Target.transform;



            _distanceToTarget = Vector3.Distance(trans.position, tTrans.position) + 0.3f;
        }

        public override TaskStatus OnUpdate()
        {
            _angle = GetAngle();
            var offsetDegrees = 25 * Mathf.Deg2Rad;
            var offset = new Vector3(Mathf.Sin(_angle - offsetDegrees), 0, Mathf.Cos(_angle - offsetDegrees)) * _distanceToTarget;

            var newPos = tTrans.position + offset;

            _movement.Speed = 1f;
            _movement.TowardsPosition = newPos;

            return TaskStatus.RUNNING;
        }

        private float GetAngle()
        {
            Vector3 difference = trans.position - tTrans.position;
            var distance = Vector3.Distance(trans.position, tTrans.position);
            var angle = Mathf.Atan2(difference.x, trans.position.z);
            return angle;
        }
    }
}

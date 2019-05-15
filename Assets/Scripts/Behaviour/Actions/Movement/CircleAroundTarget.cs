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

        private float _distanceToTarget;

        private Transform _trans;
        private Transform _tTrans;

        public override void OnStart()
        {
            _movement = gameObject.GetComponent<Entities.Movement>();

            _trans = gameObject.transform;
            _tTrans = Target.transform;

            _distanceToTarget = Vector3.Distance(_trans.position, _tTrans.position);
        }

        public override TaskStatus OnUpdate()
        {
            var angle = Vector3.SignedAngle(_trans.position - _tTrans.position, _tTrans.forward, _tTrans.up) * Mathf.Deg2Rad;
            const float offsetDegrees = 25 * Mathf.Deg2Rad;
            var offset = new Vector3(Mathf.Sin(-angle - offsetDegrees), 0, Mathf.Cos(-angle - offsetDegrees)) * _distanceToTarget;
            var newPos = _tTrans.position + offset;

            Debug.Log(angle * Mathf.Rad2Deg);

            _movement.TowardsPosition = newPos;

            return TaskStatus.RUNNING;
        }
    }
}

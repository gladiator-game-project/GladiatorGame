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

            Vector3 difference = trans.position - tTrans.position;
            var distance = Vector3.Distance(trans.position, tTrans.position);

            var angle = Mathf.Asin(difference.x / distance);

            _angle = angle - 1 * Time.deltaTime;
            _distanceToTarget = 2.5f;// Vector3.Distance(trans.position, tTrans.position);
        }

        public override TaskStatus OnUpdate()
        {
            var newPos = /*(tTrans.position - tTrans.right * 2)*/ trans.position + trans.right * 2 + trans.forward / 2;

            _movement.TowardsPosition = newPos;

            _angle -= 0.5f * Time.deltaTime;
            return TaskStatus.RUNNING;
        }
    }
}

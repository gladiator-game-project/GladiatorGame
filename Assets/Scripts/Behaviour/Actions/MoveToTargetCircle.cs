using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;

namespace Assets.Scripts.Behaviour.Actions
{
    [Action("Gladiator/Movement/ToTargetCircle")]
    [Help("Moves towards the target combat circle with a small offset to the right")]
    public class MoveToTargetCircle : GOAction
    {
        [InParam("Target")]
        [Help("Target to check the distance")]
        public GameObject Target;

        private Movement _movement;

        public override void OnStart()
        {
            _movement = gameObject.GetComponent<Movement>();
        }

        public override TaskStatus OnUpdate()
        {
            var newPos = Target.transform.position + -Target.transform.right * 3;
            _movement.TowardsPosition = newPos;

            var dist = Vector3.Distance(gameObject.transform.position, Target.transform.position);
            return dist > 3 ? TaskStatus.RUNNING : TaskStatus.FAILED;
        }
    }
}

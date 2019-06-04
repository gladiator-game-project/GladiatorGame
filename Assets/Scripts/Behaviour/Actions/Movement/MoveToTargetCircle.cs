using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;

namespace Assets.Scripts.Behaviour.Actions.Movement
{
    [Action("Gladiator/Movement/ToTargetCircle")]
    [Help("Moves towards the target combat circle with a small offset to the right")]
    public class ToTargetCircle : GOAction
    {
        [InParam("Target")]
        [Help("Target to check the distance")]
        public GameObject Target;

        private Entities.Movement _movement;

        public override void OnStart()
        {
            _movement = gameObject.GetComponent<Entities.Movement>();
        }

        public override TaskStatus OnUpdate()
        {
            var newPos = Target.transform.position + -Target.transform.right * 3;
            _movement.TowardsPosition = newPos;

            var dist = Vector3.Distance(gameObject.transform.position, Target.transform.position);
            return dist > 3 ? TaskStatus.COMPLETED : TaskStatus.FAILED;
        }
    }
}

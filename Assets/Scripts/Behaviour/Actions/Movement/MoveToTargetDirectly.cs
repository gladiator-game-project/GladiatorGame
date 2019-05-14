using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;

namespace Assets.Scripts.Behaviour.Actions.Movement
{
    [Action("Gladiator/Movement/ToTargetDirectly")]
    [Help("Moves towards the target in a straight line")]
    public class MoveToTargetDirectly : GOAction
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
            if (Vector3.Distance(gameObject.transform.position, Target.transform.position) < 3)
                return TaskStatus.COMPLETED;

            _movement.TowardsPosition = Target.transform.position;
            return TaskStatus.RUNNING;
        }
    }
}

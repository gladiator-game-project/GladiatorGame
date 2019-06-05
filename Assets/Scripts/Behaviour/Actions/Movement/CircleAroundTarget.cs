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

        //Buffer used to prevent AI from switching behaviours too fast
        private float _buffer = 1;

        public override void OnStart()
        {
            _movement = gameObject.GetComponent<Entities.Movement>();
        }

        
        public override TaskStatus OnUpdate()
        {
            var newPos =
                gameObject.transform.localPosition +
                gameObject.transform.right * 2;

            var distance = Vector3.Distance(gameObject.transform.position, Target.transform.position);

            //If the AI gets too far, set the target to the player
            if (distance >= 4 + _buffer)
            {
                _buffer -= distance - 4;
                newPos = Target.transform.position;
            }
            else
                _buffer = 1;

            _movement.TowardsPosition = newPos;
            return TaskStatus.COMPLETED;
        }
    }
}

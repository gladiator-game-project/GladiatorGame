using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Player;
using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;

namespace Assets.Scripts.Behaviour.Actions
{
    [Action("Gladiator/Interaction/Attack")]
    [Help("Plays the attack animation")]
    public class Attack : GOAction
    {
        private AnimationHandler _animHandler;
        private Entity _entity;

        public override void OnStart()
        {
            _animHandler = gameObject.GetComponent<AnimationHandler>();
            _entity = gameObject.GetComponent<Entity>();
        }

        public override TaskStatus OnUpdate()
        {
            if (_animHandler.IsAnimationRunning("attack") == false)
            {
                if (_entity.LoseStamina(40))
                    _entity.Attack(PlayerMovement.Direction.LEFT);
            }

            return TaskStatus.COMPLETED;
        }
    }
}

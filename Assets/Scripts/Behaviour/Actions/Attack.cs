using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Player;
using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;

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
            //If the animation is not running and the entity can lose 40 stamina
            if (_animHandler.IsAnimationRunning("attack") == false)
                if (_entity.LoseStamina(40))
                    _entity.Attack(Direction.LEFT);

            return TaskStatus.COMPLETED;
        }
    }
}

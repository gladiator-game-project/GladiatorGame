using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Player;
using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;

namespace Assets.Scripts.Behaviour.Actions
{
    [Action("Gladiator/AttackTarget")]
    [Help("Attacks the target")]
    public class AttackTarget : GOAction
    {
        [InParam("Target")]
        [Help("Target to check the distance")]
        public GameObject Target;

        private Entity _entity;
        private Animator _anim;

        public override void OnStart()
        {
            _entity = gameObject.GetComponent<Entities.Entity>();
            _anim = gameObject.GetComponent<Animator>();
        }

        public override TaskStatus OnUpdate()
        {
            if (_anim.GetCurrentAnimatorStateInfo(0).IsTag("attack") == false && _entity.LoseStamina(40))
            {
                _entity.Attack(PlayerMovement.Direction.LEFT);
                return TaskStatus.RUNNING;
            }
            return TaskStatus.COMPLETED;
        }
    }
}

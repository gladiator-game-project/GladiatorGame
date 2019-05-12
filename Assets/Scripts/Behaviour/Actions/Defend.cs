using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;

namespace Assets.Scripts.Behaviour.Actions
{
    [Action("Gladiator/Defend")]
    [Help("Pulls up the shield")]
    public class Defend : GOAction
    {
        private Entities.Entity _entity;

        public override void OnStart()
        {
            _entity = GameObject.Find("Player").GetComponent<Entities.Entity>();
        }

        public override TaskStatus OnUpdate()
        {
            //TODO
            return TaskStatus.FAILED;
        }

    }
}

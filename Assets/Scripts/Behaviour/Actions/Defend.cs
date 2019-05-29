using Assets.Scripts.Entities;
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
        private AnimationHandler _animHandler;

        public override void OnStart()
        {
            _animHandler = gameObject.GetComponent<AnimationHandler>();
        }

        public override TaskStatus OnUpdate()
        {
            if (_animHandler.IsAnimationRunning("defend") == false)
                _animHandler.RaiseDefense();

            return TaskStatus.COMPLETED;
        }
    }
}

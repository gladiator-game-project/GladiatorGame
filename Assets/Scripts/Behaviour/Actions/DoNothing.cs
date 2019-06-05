using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Player;
using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;

namespace Assets.Scripts.Behaviour.Actions
{
    [Action("Gladiator/Interaction/DoNothing")]
    [Help("Does nothing")]
    public class DoNothing : GOAction
    {
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}
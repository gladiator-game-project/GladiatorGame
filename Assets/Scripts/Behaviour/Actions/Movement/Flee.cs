using Pada1.BBCore;
using BBUnity.Actions;
using UnityEngine;
using Assets.Scripts.Entities;
using Pada1.BBCore.Tasks;

[Action("Gladiator/Movement/Flee")]
[Help("Run away from the target")]
public class Flee : GOAction
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
        var myPos = gameObject.transform.position;
        var targPos = Target.transform.position;

        _movement.TowardsPosition = myPos + (myPos - targPos); 
        return TaskStatus.RUNNING;
    }

}

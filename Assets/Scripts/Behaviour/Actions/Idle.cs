using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using BBUnity.Actions;

[Action("Gladiator/Idle")]
[Help("Stand around")]
public class Idle : GOAction
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
        _movement.TowardsPosition = gameObject.transform.position;
        return TaskStatus.RUNNING;
    }

}

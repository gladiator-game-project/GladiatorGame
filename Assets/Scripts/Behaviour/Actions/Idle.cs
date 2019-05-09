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

    public override TaskStatus OnUpdate()
    {        
        return TaskStatus.RUNNING;
    }

}

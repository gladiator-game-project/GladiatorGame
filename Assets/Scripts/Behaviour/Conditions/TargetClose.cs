using BBUnity.Conditions;
using Pada1.BBCore;
using UnityEngine;

[Condition("Perception/IsTargetClose")]
[Help("Checks whether the target is close depending on a given distance")]
public class TargetClose : GOCondition
{
    [InParam("Target")]
    [Help("Target to check the distance")]
    public GameObject Target;

    public override bool Check()
    {
        var distance = Vector3.Distance(gameObject.transform.position, Target.transform.position);
        return distance < 3.2f;
    }
}

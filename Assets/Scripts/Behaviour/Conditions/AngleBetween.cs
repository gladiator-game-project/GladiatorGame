using BBUnity.Conditions;
using Pada1.BBCore;
using UnityEngine;

[Condition("AngleBetween")]
[Help("Checks for the angle between two objects")]
public class AngleBetween : GOCondition
{
    [InParam("Object 1")]
    [Help("The object comparing from")]
    public GameObject AngleFrom;

    [InParam("Object 2")]
    [Help("The object comparing to")]
    public GameObject AngleTo;

    [InParam("Angle Treshold")]
    [Help("The maximum amount of degrees")]
    public int AngleThreshold;

    [InParam("Within Angle")]
    [Help("Whether to return if within the angle or outside")]
    public bool InAngle;

    public override bool Check()
    {
        var object1 = AngleFrom.transform;
        var object2 = AngleTo.transform;

        float angle = Vector3.Angle(object2.position - object1.position, object1.forward);
        return InAngle ? angle < AngleThreshold : angle > AngleThreshold;
    }
}


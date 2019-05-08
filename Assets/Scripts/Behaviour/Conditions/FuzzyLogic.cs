using BBUnity.Conditions;
using Pada1.BBCore;
using UnityEngine;

[Condition("FuzzyLogic")]
[Help("Checks for a fuzzy logic condition")]
public class FuzzyLogic : GOCondition
{
    [InParam("FuzzyConditions")]
    [Help("Target to check the distance")]
    public GameObject[] FuzzyConditions;

    public override bool Check()
    {
        return true;
    }
}


using Assets.Scripts.Entities;
using BBUnity.Conditions;
using Pada1.BBCore;
using UnityEngine;

[Condition("FuzzyLogic")]
[Help("Checks for a fuzzy logic condition")]
public class FuzzyLogic : GOCondition
{
    [InParam("DecisionHandler")]
    [Help("Our very own entity")]
    public DecisionHandler DecisionHandler;

    [InParam("Action")]
    [Help("The action we want to check against")]
    public string Action;

    [InParam("Movement")]
    [Help("Movement or Interaction")]
    public bool Movement;

    public override bool Check()
    {
        return Movement ? DecisionHandler.CurrentMovementAction == Action : DecisionHandler.CurrentInteractionAction == Action;
    }
}


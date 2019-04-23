using BBUnity.Conditions;
using Pada1.BBCore;
using UnityEngine;

[Condition("Perception/IsPlayerClose")]
[Help("Checks whether the player is close depending on a given distance")]
public class PlayerClose : GOCondition
{
    [InParam("player")]
    [Help("Player to check the distance")]
    public GameObject player;

    public override bool Check()
    {
        var distance = Vector2.Distance(gameObject.transform.position, player.transform.position);
        return distance < 1.5;
    }
}

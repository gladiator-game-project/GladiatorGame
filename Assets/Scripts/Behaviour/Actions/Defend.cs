using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using BBUnity.Actions;

[Action("Gladiator/Defend")]
[Help("Pulls up the shield")]
public class Defend : GOAction
{
    private Entity _entity;

    public override void OnStart()
    {
        _entity = GameObject.Find("Player").GetComponent<Entity>();
    }

    public override TaskStatus OnUpdate()
    {
        //TODO
        return TaskStatus.FAILED;
    }

}

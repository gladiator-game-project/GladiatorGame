using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using BBUnity.Actions;

[Action("Gladiator/AttackTarget")]
[Help("Attacks the target")]
public class AttackTarget : GOAction
{
    [InParam("Target")]
    [Help("Target to check the distance")]
    public GameObject Target;

    private Entity _entity;

    public override void OnStart()
    {
        _entity = gameObject.GetComponent<Entity>();
    }

    public override TaskStatus OnUpdate()
    {
        _entity.Attack(PlayerMovement.Direction.Right);

        

        return TaskStatus.COMPLETED;
    }

}

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

    public override TaskStatus OnUpdate()
    {
        Entity entity = Target.GetComponent<Entity>();

        if (entity != null)
        {
            entity.Health -= 1;
            return TaskStatus.COMPLETED;
        }
        return TaskStatus.FAILED;
    }

}

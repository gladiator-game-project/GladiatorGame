using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using BBUnity.Actions;

[Action("Gladiator/AttackPlayer")]
[Help("Attacks the player")]
public class AttackPlayer : GOAction
{
    private Entity _entity;

    public override void OnStart()
    {
        _entity = GameObject.Find("Player").GetComponent<Entity>();
    }

    public override TaskStatus OnUpdate()
    {
        if (_entity != null)
        {
            _entity.LoseHealth(1);
            return TaskStatus.COMPLETED;
        }
        return TaskStatus.FAILED;
    }

}

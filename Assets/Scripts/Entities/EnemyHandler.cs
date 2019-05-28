using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Entities;

public class EnemyHandler : MonoBehaviour
{
    private Entity _entitiyscript;
    // Start is called before the first frame update
    void Start()
    {
        _entitiyscript = GetComponent<Entity>();
        SetRagdoll(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_entitiyscript.Alive == false)
        {
            SetRagdoll(true);
            Destroy(this, 3f);
        }
    }

    void SetRagdoll(bool onoff)
    {
        Rigidbody[] Rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigidbody in Rigidbodies)
        {
            rigidbody.isKinematic = !onoff;
            rigidbody.useGravity = onoff;
        }
        GetComponent<BehaviorExecutor>().enabled = !onoff;
        GetComponent<AnimationHandler>().enabled = !onoff;
        GetComponent<Movement>().enabled = !onoff;
        GetComponent<WeaponHandler>().enabled = !onoff;
        GetComponent<DamageHandler>().enabled = !onoff;
        GetComponent<Animator>().enabled = !onoff;
    }
}

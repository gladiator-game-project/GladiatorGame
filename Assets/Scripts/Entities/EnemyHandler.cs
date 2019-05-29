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
    }

    // Update is called once per frame
    void Update()
    {
        if (_entitiyscript.Alive == false)
            GoDie();
    }
 
    private void GoDie()
    {
        GameObject dead = Instantiate(Resources.Load("Prefabs/deathNPC", typeof(GameObject))) as GameObject;
        // copy position and rotation to the children recursively:
        CopyTransforms(transform, dead.transform);
        Destroy(gameObject);
        
    }
    private void CopyTransforms(Transform old, Transform newtransform)
    {
        newtransform.position = old.position;
        newtransform.rotation = old.rotation;
        foreach (Transform child in newtransform)
        {
            // match the transform with the same name
            var curSrc = old.Find(child.name);
            if (curSrc)
                CopyTransforms(curSrc, child);
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

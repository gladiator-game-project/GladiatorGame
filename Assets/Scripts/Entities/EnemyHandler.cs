using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Entities;

public class EnemyHandler : MonoBehaviour
{
    private Entity _entityscript;
    // Start is called before the first frame update
    void Start()
    {
        _entityscript = GetComponent<Entity>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_entityscript.Alive == false)
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
}

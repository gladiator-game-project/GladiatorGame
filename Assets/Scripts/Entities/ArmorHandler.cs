using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorHandler : MonoBehaviour
{
    public bool HasHelmet;
    public bool HasSkirt;
    public bool HasChest;

    // Start is called before the first frame update
    void Awake()
    {
        if (!HasChest)
            transform.Find("Armature/Hips/Spine/Chest").gameObject.SetActive(false);
        
        if(!HasSkirt)
            transform.Find("Armature/Hips/Skirt").gameObject.SetActive(false);

        if(!HasHelmet)
            transform.Find("Armature/Hips/Spine/Spine1/Spine2/Neck/Head/Helmet").gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorHandler : MonoBehaviour
{
    public bool HasHelmet;
    public bool HasSkirt;
    public bool HasChest;
    public bool HasShield;

    // Start is called before the first frame update
    void Awake()
    {
        if (!HasChest)
            transform.Find("Body/Chest").gameObject.SetActive(false);
        
        if(!HasSkirt)
            transform.Find("Body/Skirt").gameObject.SetActive(false);

        if(!HasHelmet)
            transform.Find("Body/Helmet").gameObject.SetActive(false);

        if (!HasShield)
            transform.Find("Armature/Hips/Spine/Spine1/LeftShoulder/LeftArm/LeftLowerArm/LeftHand/shield").gameObject.SetActive(false);

    }
}

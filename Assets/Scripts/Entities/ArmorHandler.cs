using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorHandler : MonoBehaviour
{
    public GameObject Chest;
    public GameObject Skirt;
    public GameObject Helmet;
    public GameObject Shield;

    public bool HasHelmet;
    public bool HasSkirt;
    public bool HasChest;
    public bool HasShield;

    void Awake()
    {
        Chest = transform.Find("Body/Chest").gameObject;
        Skirt = transform.Find("Body/Skirt").gameObject;
        Helmet = transform.Find("Body/Helmet").gameObject;
        Shield = transform.Find("Armature/Hips/Spine/Spine1/LeftShoulder/LeftArm/LeftLowerArm/LeftHand/shield").gameObject;
        
        UpdateArmor();
    }

    public void UpdateArmor()
    {
        Chest.SetActive(HasChest);
        Skirt.SetActive(HasSkirt);
        Helmet.SetActive(HasHelmet);
        Shield.SetActive(HasShield);
    }
}

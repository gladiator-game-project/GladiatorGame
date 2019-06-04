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
        UpdateArmor();
    }

    public void UpdateArmor()
    {
        transform.Find("Body/Chest").gameObject.SetActive(HasChest);
        transform.Find("Body/Skirt").gameObject.SetActive(HasSkirt);
        transform.Find("Body/Helmet").gameObject.SetActive(HasHelmet);
        transform.Find("Armature/Hips/Spine/Spine1/LeftShoulder/LeftArm/LeftLowerArm/LeftHand/shield").gameObject.SetActive(HasShield);
    }
}

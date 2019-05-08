using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeWeapon : MonoBehaviour
{
    private GameObject CurrentWeapon;
    private GameObject SelectedWeapon;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponent<Entity>().weapon != null)
            CurrentWeapon = gameObject.GetComponent<Entity>().weapon.gameObject;
        else
            CurrentWeapon = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckForWeapon() && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Grab weapon");
            SwitchWeapon();
        }
    }

    private void SwitchWeapon()
    {
        if (CurrentWeapon != null)
        {
            SelectedWeapon.transform.SetParent(CurrentWeapon.transform.parent);
            SelectedWeapon.transform.position = CurrentWeapon.transform.position;
            Rigidbody New_weapon = SelectedWeapon.GetComponent<Rigidbody>();
            New_weapon.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ |
            RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
            New_weapon.useGravity = false;
            CurrentWeapon.transform.parent = null;
            Rigidbody Old_weapon = CurrentWeapon.GetComponent<Rigidbody>();
            Old_weapon.constraints = RigidbodyConstraints.None;
            Old_weapon.useGravity = true;
            gameObject.GetComponent<Entity>().weapon = SelectedWeapon.gameObject.GetComponent<BaseWeapon>();
            CurrentWeapon = SelectedWeapon;
            CurrentWeapon.transform.localRotation = CurrentWeapon.GetComponent<BaseWeapon>().originalRotation;
            CurrentWeapon.GetComponent<DoDamage>().animator = gameObject.GetComponent<Animator>();
            CurrentWeapon.GetComponent<BaseWeapon>().animator = gameObject.GetComponent<Animator>();
            SelectedWeapon = null;
        }
    }

    private bool CheckForWeapon()
    {
        SelectedWeapon = null;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 6.0f))
        {
            Collider[] hitColliders = Physics.OverlapSphere(hit.point, 2.01F);
            int i = 0;
            while (i < hitColliders.Length) // c
            {
                if (hitColliders[i].tag == "Weapon" && hitColliders[i].gameObject != CurrentWeapon)
                {
                    SelectedWeapon = hitColliders[i].gameObject;
                    return true;
                }
                i++;
            }
            return false;
        }
        return false;
            
    }
}

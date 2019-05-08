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

        }
    }

    private bool CheckForWeapon()
    {
        SelectedWeapon = null;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 6.0f))
        {
            Collider[] hitColliders = Physics.OverlapSphere(hit.point, 2.01F); // check alle colliders in een radius van 4.01 vanuit de buidling
            int i = 0;
            while (i < hitColliders.Length) // check elke collider of het een road is, en als het erin zit is het true en mag het geplaats worden
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

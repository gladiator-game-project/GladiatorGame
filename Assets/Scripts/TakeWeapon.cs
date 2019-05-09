using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeWeapon : MonoBehaviour
{
    private GameObject _currentWeapon;
    private GameObject _selectedWeapon;

    public GameObject TextBar;
    private Text btnText;
    // Start is called before the first frame update
    void Start()
    {
        btnText = TextBar.GetComponent<Text>();

        if (gameObject.GetComponent<Entity>().weapon != null)
            _currentWeapon = gameObject.GetComponent<Entity>().weapon.gameObject;
        else
            _currentWeapon = null;
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
        if (_currentWeapon != null)
        {
            //change positions for weapon and rigidbody settings
            _selectedWeapon.transform.SetParent(_currentWeapon.transform.parent);
            _selectedWeapon.transform.position = _currentWeapon.transform.position;
            Rigidbody New_weapon = _selectedWeapon.GetComponent<Rigidbody>();
            New_weapon.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ |
            RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
            New_weapon.useGravity = false;
            _currentWeapon.transform.parent = null;
            Rigidbody Old_weapon = _currentWeapon.GetComponent<Rigidbody>();
            Old_weapon.constraints = RigidbodyConstraints.None;
            Old_weapon.useGravity = true;
            gameObject.GetComponent<Entity>().weapon = _selectedWeapon.gameObject.GetComponent<BaseWeapon>();
            //officialy make new weapon the current weapon
            _currentWeapon = _selectedWeapon;
            _currentWeapon.transform.localRotation = _currentWeapon.GetComponent<BaseWeapon>().originalRotation;
            _currentWeapon.GetComponent<DoDamage>().animator = gameObject.GetComponent<Animator>();
            _currentWeapon.GetComponent<BaseWeapon>().animator = gameObject.GetComponent<Animator>();
            _selectedWeapon = null;
        }
    }

    private bool CheckForWeapon()
    {
        _selectedWeapon = null;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 6.0f)) // 6.0 is the range of the cast
        {
            Collider[] hitColliders = Physics.OverlapSphere(hit.point, 2.01F);
            int i = 0;
            while (i < hitColliders.Length) 
            {
                if (hitColliders[i].tag == "Weapon" && hitColliders[i].gameObject != _currentWeapon)
                {
                    _selectedWeapon = hitColliders[i].gameObject;
                    btnText.text = "Press E to pick up " + hitColliders[i].name;
                    return true;
                }
                i++;
            }
            btnText.text = "";
            return false;
        }
        btnText.text = "";
        return false;
    }
}

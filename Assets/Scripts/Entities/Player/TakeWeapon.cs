using Assets.Scripts.Weapon;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Entities.Player
{
    public class TakeWeapon : MonoBehaviour
    {
        private GameObject _currentWeapon;
        private GameObject _selectedWeapon;

        public GameObject TextBar;
        private Text _btnText;
        // Start is called before the first frame update
        void Start()
        {
            _btnText = TextBar.GetComponent<Text>();
            _currentWeapon = gameObject.GetComponent<Entities.Entity>().Weapon.gameObject;
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

                var newWeapon = _selectedWeapon.GetComponent<Rigidbody>();
                newWeapon.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ |
                                         RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
                newWeapon.useGravity = false;

                _currentWeapon.transform.parent = null;

                var oldWeapon = _currentWeapon.GetComponent<Rigidbody>();
                oldWeapon.constraints = RigidbodyConstraints.None;
                oldWeapon.useGravity = true;

                gameObject.GetComponent<Entities.Entity>().Weapon = _selectedWeapon.gameObject.GetComponent<BaseWeapon>();
                
                //officialy make new weapon the current weapon
                _currentWeapon = _selectedWeapon;
                _currentWeapon.transform.localRotation = _currentWeapon.GetComponent<BaseWeapon>().OriginalRotation;
                _currentWeapon.GetComponent<DoDamage>().Animator = gameObject.GetComponent<Animator>();
                _currentWeapon.GetComponent<BaseWeapon>().Animator = gameObject.GetComponent<Animator>();
                _selectedWeapon = null;
            }
        }

        private bool CheckForWeapon()
        {
            _selectedWeapon = null;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, 6.0f)) // 6.0 is the range of the cast
            {
                var hitColliders = Physics.OverlapSphere(hit.point, 2.01F);
                var i = 0;

                while (i < hitColliders.Length) 
                {
                    if (hitColliders[i].tag == "Weapon" && hitColliders[i].gameObject != _currentWeapon)
                    {
                        _selectedWeapon = hitColliders[i].gameObject;
                        _btnText.text = "Press E to pick up " + hitColliders[i].name;
                        return true;
                    }
                    i++;
                }
            }
            _btnText.text = "";
            return false;
        }
    }
}

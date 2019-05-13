using Assets.Scripts.Weapon;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Entities.Player
{
    public class TakeWeapon : MonoBehaviour
    {
        private Entity _playerEntity;
        private GameObject _currentWeapon;
        private GameObject _selectedWeapon;

        public GameObject PickupImage;

        public void Start()
        {
            _playerEntity = gameObject.GetComponent<Entity>();
        }

        public void Update()
        {
            if (CheckForWeapon() && Input.GetKeyDown(KeyCode.E))
                SwitchWeapon();
        }

        private void SwitchWeapon()
        {
            _currentWeapon = _playerEntity.Weapon.gameObject;

            if (_currentWeapon == null)
                return;

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

            gameObject.GetComponent<Entity>().Weapon = _selectedWeapon.gameObject.GetComponent<BaseWeapon>();

            //officialy make new weapon the current weapon
            _currentWeapon = _selectedWeapon;
            var baseWeapon = _currentWeapon.GetComponent<BaseWeapon>();
            _currentWeapon.transform.localPosition = baseWeapon.Position;
            _currentWeapon.transform.localEulerAngles = baseWeapon.Rotation;
            _selectedWeapon = null;
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
                    if (hit.transform.GetComponentInParent<Entity>() != null)
                        break;

                    if (hitColliders[i].tag == "Weapon" && hitColliders[i].gameObject != _currentWeapon)
                    {
                        _selectedWeapon = hitColliders[i].gameObject;
                        PickupImage.SetActive(true);
                        //_btnText.text = "Press E to pick up " + hitColliders[i].name;
                        return true;
                    }
                    i++;
                }
            }
            PickupImage.SetActive(false);
            return false;
        }
    }
}

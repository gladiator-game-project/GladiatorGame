using Assets.Scripts.Weapon;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Entities.Player
{
    public class WeaponHandler : MonoBehaviour
    {
        public GameObject Hand;
        public BaseWeapon Weapon;
        public GameObject PickupImage;

        private GameObject _currentWeapon;
        private GameObject _selectedWeapon;
        private AnimationHandler _animHandler;

        public void Start()
        {
            _animHandler = GetComponent<AnimationHandler>();
        }

        public void Update()
        {
            if (CheckForWeapon() && Input.GetKeyDown(KeyCode.E))
                SwitchWeapon();
            UpdateWeapon();
            _animHandler.SetIdle(Weapon.CurrentType);
        }

        private void SwitchWeapon()
        {
            _currentWeapon = Weapon.gameObject;
            var entityColliders = GetComponentsInChildren<Collider>();


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
            IgnorePhysics(oldWeapon.GetComponentsInChildren<Collider>(), entityColliders, false);

            Weapon = _selectedWeapon.gameObject.GetComponent<BaseWeapon>();

            _animHandler.SetIdle(Weapon.CurrentType);


            //officialy make new weapon the current weapon
            _currentWeapon = _selectedWeapon;
            var baseWeapon = _currentWeapon.GetComponent<BaseWeapon>();
            _currentWeapon.transform.localPosition = baseWeapon.Position;
            _currentWeapon.transform.localRotation = Quaternion.Euler(baseWeapon.Rotation);

            //The weapon should ignore the entity colliders
            Collider[] weaponColliders = _currentWeapon.GetComponentsInChildren<Collider>();
            IgnorePhysics(weaponColliders, entityColliders, true);


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
                    {
                        if (hit.transform.GetComponentInParent<Entity>().CompareTag("Enemy") || hit.transform.GetComponentInParent<Entity>().CompareTag("Player"))
                        {
                            break;
                        }
                    }


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

            if(PickupImage != null)
            {
                PickupImage.SetActive(false);
            }
            return false;
        }

        private void UpdateWeapon()
        {
            if (Weapon != null)
                return;

            var knucklesPrefab = (GameObject)Instantiate(Resources.Load("Prefabs/Weapons/knuckles"));
            knucklesPrefab.transform.parent = Hand.transform;
            knucklesPrefab.transform.localPosition = new Vector3(0.0f, 0.002f, 0.0f);

            Weapon = knucklesPrefab.GetComponent<BaseWeapon>();
            Weapon.CurrentType = BaseWeapon.AttackType.PUNCH;


            var playerColliders = GetComponentsInChildren<Collider>();
            var knuckleColliders = knucklesPrefab.GetComponents<Collider>();

            IgnorePhysics(knuckleColliders, playerColliders, true);
        }

        private void IgnorePhysics(Collider[] weaponColliders, Collider[] entityColliders, bool ignore)
        {
            foreach (var col in entityColliders)
            {
                foreach (var weaponCol in weaponColliders)
                {
                    Physics.IgnoreCollision(weaponCol, col, ignore);
                }
            }
        }
    }
}

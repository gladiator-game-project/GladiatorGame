using Assets.Scripts.Weapon;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class Entity : MonoBehaviour
    {
        public float MaxHealth;
        public float MaxStamina;

        public GameObject Hand;
        public BaseWeapon Weapon;
        private Animator _animator;

        public bool HasShield;
        public bool Alive = true;

        private float _health;
        private float _stamina;
        private float _courage;

        public void Start()
        {
            _animator = GetComponent<Animator>();
            Health = MaxHealth;
            Stamina = MaxStamina;
            _courage = 100;
            InvokeRepeating("RegainStamina", 2.0f, 2.0f); // repeat function
        }

        public void Update()
        {
            UpdateDeath();
            UpdateWeapon();
        }

        public float Health
        {
            get => _health;
            set => _health = Mathf.Clamp(value, 0, MaxHealth);
        }

        public float Stamina
        {
            get => _stamina;
            set => _stamina = Mathf.Clamp(value, 0, MaxStamina);
        }

        public void LowerDefense()
        {
            _animator.SetBool(HasShield ? "HoldShield" : "HoldSword", false);
        }

        public void RaiseDefense()
        {
            if (HasShield)
            {
                _animator.SetTrigger("RaiseShield");
                _animator.SetBool("HoldShield", true);
            }
            else
            {
                _animator.SetTrigger("RaiseSword");
                _animator.SetBool("HoldSword", true);
            }

        }

        public float Courage => _courage + _health - 100;

        public void Attack(PlayerMovement.Direction direction)
        {
            Health = MaxHealth;
            Stamina = MaxStamina;
            Alive = true;
            InvokeRepeating("RegainStamina", 2.0f, 2.0f); // repeat function
            _animator = GetComponent<Animator>();
            Weapon.Attack(direction);
        }

        public bool LoseStamina(float stamina)
        {
            Stamina -= Stamina - stamina > 0 ? stamina : 0;
            return Stamina - stamina > 0;
        }

        private void RegainStamina()
        {
            Stamina += 20;
        }

        private void UpdateDeath()
        {
            if (_health <= 0)
                Alive = false;
        }

        private void UpdateWeapon()
        {
            if (Weapon == null)
            {
                GameObject knucklesPrefab = (GameObject)Instantiate(Resources.Load("Prefabs/Weapons/knuckles"));
                knucklesPrefab.transform.parent = Hand.transform;
                Weapon = knucklesPrefab.GetComponent<BaseWeapon>();
                Weapon.CurrentType = BaseWeapon.AttackType.PUNCH;
            }
        }

    }
}

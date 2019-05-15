using System.Linq;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Weapon;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class Entity : MonoBehaviour
    {
        public float MaxHealth;
        public float MaxStamina;

        public bool Alive = true;

        private AnimationHandler _animHandler;
        private WeaponHandler _weaponHandler;
        private float _health;
        private float _stamina;
        private float _courage;

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

        public float Courage =>
            _courage + _health - 100;

        public void Start()
        {
            _animHandler = GetComponent<AnimationHandler>();
            _weaponHandler = GetComponent<WeaponHandler>();

            Health = MaxHealth;
            Stamina = MaxStamina;
            _courage = 100;
            InvokeRepeating("RegainStamina", 2.0f, 2.0f); // repeat function
        }

        public void Update()
        {
            UpdateDeath();
        }

        public void RaiseDefense() =>
            _animHandler.RaiseDefense();

        public void LowerDefense() =>
            _animHandler.LowerDefense();

        public void Attack(PlayerMovement.Direction direction) =>
            _animHandler.Attack(_weaponHandler.Weapon.CurrentType, direction);

        public bool LoseStamina(float stamina)
        {
            Stamina -= Stamina - stamina > 0 ? stamina : 0;
            return Stamina - stamina > 0;
        }

        private void RegainStamina() =>
            Stamina += 20;

        private void UpdateDeath()
        {
            if (Health > 0)
                return;

            Alive = false;
            _animHandler.SetEnabled(false);
        }
    }
}

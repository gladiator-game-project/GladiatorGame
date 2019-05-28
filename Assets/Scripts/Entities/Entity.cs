using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Weapon;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class Entity : MonoBehaviour
    {
        public List<string> UsingStamina;

        public float MaxHealth;
        public float MaxStamina;

        public bool Alive = true;        

        private AnimationHandler _animHandler;
        private WeaponHandler _weaponHandler;
        private float _health;
        private float _stamina;
        private float _courage;

        private float _timer;
        private bool _startedStamina = false;

        public float Health
        {
            get => _health;
            set
            {
                _health = Mathf.Clamp(value, 0, MaxHealth);

                if (Health <= 0)
                {
                    Alive = false;
                    _animHandler.SetEnabled(false);
                }
            }
        }

        public float Stamina
        {
            get => _stamina;
            set => _stamina = Mathf.Clamp(value, 0, MaxStamina);
        }

        public float Courage
        {
            get => _courage + _health - 100;
            set => _courage = Mathf.Clamp(value, 0, 100);
        }

        public void Start()
        {
            _animHandler = GetComponent<AnimationHandler>();
            _weaponHandler = GetComponent<WeaponHandler>();
            UsingStamina = new List<string>();

            Health = MaxHealth;
            Stamina = MaxStamina;

            _courage = 100;
            _timer = 0.0f;
        }

        public void Update()
        {
            UpdateStaminaRegen();
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

        private void UpdateStaminaRegen()
        {
            if (UsingStamina.Count != 0)
            {
                _timer = 0.0f;
                CancelInvoke("RegainStamina");
                _startedStamina = false;
            }
            else
                _timer += Time.deltaTime;

            int seconds = (int)_timer % 60;
            if (seconds == 5 && !_startedStamina) // change that 5 to cooldown seconds
            {
                InvokeRepeating("RegainStamina", 0.0f, 2.0f); // repeat function
                _startedStamina = true;
            }
        }

        private void RegainStamina() =>
            Stamina += 20;

        private void Die()
        {
            gameObject.GetComponent<Animator>().enabled = false;
            setRigidbodyState(false);
            setColliderState(true);

            if (gameObject != null)
            {
                Destroy(gameObject, 3f);
            }

        }

        void setRigidbodyState(bool state)
        {

            Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

            foreach (Rigidbody rigidbody in rigidbodies)
            {
                rigidbody.isKinematic = state;
            }

            GetComponent<Rigidbody>().isKinematic = !state;

        }


        void setColliderState(bool state)
        {

            Collider[] colliders = GetComponentsInChildren<Collider>();

            foreach (Collider collider in colliders)
            {
                collider.enabled = state;
            }

            GetComponent<Collider>().enabled = !state;

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Entities.Player;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class Entity : MonoBehaviour
    {
        public List<string> UsingStamina;

        public float MaxHealth;
        public float MaxStamina;

        public bool Alive = true;

        private NavAgentController _navController;
        private AnimationHandler _animHandler;
        private WeaponHandler _weaponHandler;
        private float _health;
        public float _stamina;
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
            _navController = GetComponent<NavAgentController>();
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
            _animHandler.RaiseDefence();

        public void LowerDefense() =>
            _animHandler.LowerDefence();

        public void Attack(PlayerMovement.Direction direction) =>
            _animHandler.Attack(_weaponHandler.Weapon.CurrentType, direction);

        public bool LoseStamina(float stamina)
        {
            bool enoughStamina = Stamina - stamina >= 0;
            Stamina -= enoughStamina ? stamina : 0;
            return enoughStamina;
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
                InvokeRepeating("RegainStamina", 0.0f, 0.1f); // repeat function
                _startedStamina = true;
            }
        }

        private void RegainStamina() =>
            Stamina += 1;

        public IEnumerator EndOfBattle()
        {
            //_animHandler.StartVictory();
            yield return new WaitForSeconds(1);
            _navController.SetDestination(new Vector3(-1, 0, -46));
        }
    }
}

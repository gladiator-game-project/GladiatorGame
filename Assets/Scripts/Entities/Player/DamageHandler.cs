using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Weapon;
using UnityEngine;

namespace Assets.Scripts.Entities.Player
{
    public class DamageHandler : MonoBehaviour
    {
        private List<Collider> _weaponColliders; //The list with all colliders that are colliding with the weapon.

        private float _damageImmunity = 1f;
        private float _damageImmunityTimer;
        public bool IsImmune;

        void Start()
        {
            _weaponColliders = new List<Collider>();
        }

        private void Update()
        {
            if (IsImmune == false)
                CheckDamage();
            else
                UpdateTimer();
        }

        public void OnTriggerEnter(Collider col)
        {
            var obj = col.GetComponentInParent<AnimationHandler>();
            var isWeapon = col.GetComponent<BaseWeapon>();


            if (obj == null || isWeapon == null)
                return;

            if (gameObject != obj.gameObject && _weaponColliders.Contains(col) == false)
                _weaponColliders.Add(col);
        }

        public void OnTriggerExit(Collider col)
        {
            if (_weaponColliders != null && _weaponColliders.Contains(col))
                _weaponColliders.Remove(col);
        }

        private void UpdateTimer()
        {
            if (_damageImmunityTimer >= _damageImmunity)
            {
                IsImmune = false;
                _damageImmunityTimer = 0;
            }
            _damageImmunityTimer += Time.deltaTime;
        }


        /// <summary>
        /// Checks if the entity itself is hit and by which weapon. 
        /// Then reduces hp by the amount of damage the attacking weapon does
        /// This function checks which collider has hit us.
        /// </summary>
        private void CheckDamage()
        {
            foreach (var c in _weaponColliders) //For each collider check if it has an entity script and remove if it does
            {
                if (c.GetComponentInParent<AnimationHandler>().IsAnimationRunning("attack")) // Also check if animation is playing
                {
                    int damage = c.GetComponentInParent<WeaponHandler>().Weapon.damage; // retrieve damage done by the colliding weapon 
                    var healthScript = GetComponent<Entity>(); // Call entity script of the hit entity
                    healthScript.Health -= damage; // Call the LoseHealth function from entity script
                    IsImmune = true;
                    break;
                }
            }
            _weaponColliders = new List<Collider>();
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Weapon;
using UnityEngine;

namespace Assets.Scripts.Entities.Player
{
    public class DamageHandler : MonoBehaviour
    {
        private List<Collider> _colliders; //The list with all colliders that are colliding with the weapon.

        private float _damageImmunity = 1f;
        private float _damageImmunityTimer;
        private bool _isImmune;

        void Start()
        {
            _colliders = new List<Collider>(); //Create new List
        }

        private void Update()
        {
            if (_isImmune == false)
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

            if (gameObject != obj.gameObject && _colliders.Contains(col) == false)
                _colliders.Add(col);
        }

        public void OnTriggerExit(Collider col)
        {
            if (_colliders.Contains(col))
                _colliders.Remove(col);
        }

        private void UpdateTimer()
        {
            if (_damageImmunityTimer >= _damageImmunity)
            {
                _isImmune = false;
                _damageImmunityTimer = 0;
            }
            _damageImmunityTimer += Time.deltaTime;
        }

        
        /// <summary>
        /// Checks if the entity itself is hit and by which weapon. 
        /// Then reduces hp by the amount of damage the attacking weapon does
        /// </summary>
        private void CheckDamage()
        {
            foreach (var c in _colliders) //For each collider check if it has an entity script and remove if it does
            {
                if (c.GetComponentInParent<AnimationHandler>().IsAnimationRunning("attack")) // Also check if animation is playing
                {
                    int damage = c.GetComponentInParent<WeaponHandler>().Weapon.damage; // retrieve damage done by the colliding weapon 
                    var healthScript = GetComponent<Entity>(); // Call entity script of the hit entity
                    healthScript.Health -= damage; // Call the LoseHealth function from entity script
                    Debug.Log(healthScript.Health);
                    _isImmune = true;
                    break;
                }
            }
            _colliders = new List<Collider>();
        }
    }
}

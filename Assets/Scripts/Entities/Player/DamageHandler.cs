using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Weapon;
using UnityEngine;

namespace Assets.Scripts.Entities.Player
{
    public class DamageHandler : MonoBehaviour
    {
        private AnimationHandler _animHandler;
        private List<Collider> _colliders; //The list with all colliders that are colliding with the weapon.

        void Start()
        {
            _colliders = new List<Collider>(); //Create new List
            _animHandler = gameObject.GetComponentInParent<AnimationHandler>();
        }

        private void Update()
        {
            CheckDamage();
        }

        private void CheckDamage()
        {
            foreach (var c in _colliders) //For each collider check if it has an entity script and remove if it does
            {
                if (c.GetComponentInParent<AnimationHandler>().IsAnimationRunning("attack")) // Also check if animation is playing
                {
                    const float damage = 20f; //Amount of damage
                    var healthScript = GetComponent<Entity>(); // Call entity script of the hit entity
                    healthScript.Health -= damage; // Call the LoseHealth function from entity script
                }
            }

            _colliders = new List<Collider>();
        }

        public void OnTriggerEnter(Collider col)
        {
            var obj = col.GetComponentInParent<AnimationHandler>();
            var isWeapon = col.GetComponent<BaseWeapon>();

            if (obj == null || isWeapon == null)
                return;

            if (gameObject != obj.gameObject)
                _colliders.Add(col);
        }

        public void OnTriggerExit(Collider col)
        {
            if (_colliders.Contains(col))
                _colliders.Remove(col);
        }
    }
}

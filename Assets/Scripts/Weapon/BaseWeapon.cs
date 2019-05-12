using Assets.Scripts.Entities;
using UnityEngine;

namespace Assets.Scripts.Weapon
{
    public class BaseWeapon : MonoBehaviour
    {
        public Animator Animator;
        public Quaternion OriginalRotation;

        public enum AttackType { STAB,SLASH,PUNCH}

        public AttackType CurrentType;

        private void Start()
        {
            OriginalRotation = transform.localRotation;
            Animator = GetComponentInParent<Animator>();
        }

        /// <summary>
        /// Set the correct animation based on the direction
        /// </summary>
        /// <param name="direction"></param>
        public void Attack(PlayerMovement.Direction direction)
        {
            if(CurrentType == AttackType.SLASH)
                Animator.SetTrigger("Slash" + direction);
            else
                Animator.SetTrigger(CurrentType.ToString());
            
        }
    }
}

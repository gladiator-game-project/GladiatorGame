using Assets.Scripts.Entities;
using UnityEngine;

namespace Assets.Scripts.Weapon
{
    public class BaseWeapon : MonoBehaviour
    {
        public Vector3 Position;
        public Vector3 Rotation;
        public GameObject DamageEffect;

        public enum AttackType { STAB,SLASH,PUNCH}

        public AttackType CurrentType;

        public void Start()
        {
            if(GetComponentInParent<Entity>() != null)
            {
                transform.localPosition = Position;
                transform.localEulerAngles = Rotation;
            }
        }

        public void OnCollisionEnter(Collision col)
        {
            Instantiate(DamageEffect, col.contacts[0].point, Quaternion.FromToRotation(Vector3.up, col.contacts[0].normal));
        }
    }
}

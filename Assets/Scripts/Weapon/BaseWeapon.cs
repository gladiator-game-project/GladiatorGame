using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    public Animator animator;
    public Quaternion originalRotation;

    public enum AttackType { Stab,Slash,Punch}

    public AttackType currentType;

    private void Start()
    {
        originalRotation = transform.localRotation;
        animator = GetComponentInParent<Animator>();
    }

    //default animation if a entity does not have a weapon.
    public void Attack(PlayerMovement.Direction direction)
    {
        if(currentType == AttackType.Slash)
        {
            animator.SetTrigger("Slash" + direction);
        }
        else
        {
            animator.SetTrigger(currentType.ToString());
        }
    }
}

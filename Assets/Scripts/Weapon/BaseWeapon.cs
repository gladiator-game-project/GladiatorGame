using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    public Animator animator;
    public Quaternion originalRotation;

    public enum AttackType { Stab, Punch, Slash }

    public AttackType currentAnim = AttackType.Punch;

    private void Start()
    {
        originalRotation = transform.localRotation;
        animator = GetComponentInParent<Animator>();
    }


    public void Attack(PlayerMovement.Direction direction)
    {
        if(currentAnim == AttackType.Slash)
        {
            animator.SetTrigger(currentAnim.ToString() + direction.ToString());
        }
        else
        {
            animator.SetTrigger(currentAnim.ToString());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    protected Animator animator;

    //get hash for optimalization so the location only has to be retrieved once.
    protected int stabHash = Animator.StringToHash("Stab");
    protected int slashLeftHash = Animator.StringToHash("SlashRight");
    protected int slashRightHash = Animator.StringToHash("SlashLeft");

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    //default animation if a entity does not have a weapon.
    public virtual void AttackDefault()
    {
        animator.SetTrigger(stabHash);
    }

    public virtual void AttackLeft()
    {
        animator.SetTrigger(slashLeftHash);
    }

    public virtual void AttackRight()
    {
        animator.SetTrigger(slashRightHash);
    }
}

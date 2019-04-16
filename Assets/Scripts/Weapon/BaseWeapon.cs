using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    protected Animator animator;

    //get hash for optimalization so the location only has to be retrieved once.
    protected int stabHash = Animator.StringToHash("Stab");
    protected int punchHash = Animator.StringToHash("Punch");

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    //default animation if a entity does not have a weapon.
    public virtual void Animate()
    {
        animator.SetTrigger(punchHash);
    }
}

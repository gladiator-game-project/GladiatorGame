using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamage : MonoBehaviour
{
    public GameObject damageEffect;
    private List<Collision> _collisions; //Declare list with all colliders that are colliding with the weapon.
    public Animator animator; // Declare the animator of the weapon

    void Start()
    {
        _collisions = new List<Collision>(); //Create new List
        if (gameObject.GetComponentInParent<Animator>() != null)
            animator = gameObject.GetComponentInParent<Animator>();
        else
            animator = null;
    }

    private void Update()
    {
        CheckDamage(); //Call function that checks for damage
    }

    private void CheckDamage()
    {
        if (animator == null)
            return;
        List<Collision> toRemove = new List<Collision>(); //Create list of colliders that needs to be removed from the list colliders 

        foreach(Collision c in _collisions) //For each collider check if it has an entity script and remove if it does
        {
            if(c.gameObject.GetComponent<Entity>() != null && animator.GetCurrentAnimatorStateInfo(0).IsTag("attack")) // Also check if animation is playing
            {
                Instantiate(damageEffect, c.contacts[0].point, Quaternion.FromToRotation(Vector3.up, c.contacts[0].normal));
                Entity healthScript = c.gameObject.GetComponent<Entity>(); // Call entity script of the hit entity
                int damage = 20; //Amount of damage
                healthScript.LoseHealth(damage); // Call the LoseHealth function from entity script
                toRemove.Add(c); //Add collider in a list to remove it, so it can be removed after the for each loop
            }
        }

        foreach(Collision c in toRemove)
        {
            _collisions.Remove(c); //Remove from collider list
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_collisions.Contains(collision))
            _collisions.Add(collision); //Add collider in list if the weapon hits something
    }

    private void OnCollisionExit(Collision collision)
    {
        if (_collisions.Contains(collision))
            _collisions.Remove(collision); //Remove collider when its not hitting stuff anymore.
    }
}

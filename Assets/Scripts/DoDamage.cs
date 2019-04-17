using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamage : MonoBehaviour
{
    private List<Collider> _colliders; //Declare list with all colliders that are colliding with the weapon.
    private Animator _ani; // Declare the animator of the weapon

    void Start()
    {
        _colliders = new List<Collider>(); //Create new List
        _ani = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        CheckDamage(); //Call function that checks for damage
    }

    private void CheckDamage()
    {
        List<Collider> toRemove = new List<Collider>(); //Create list of colliders that needs to be removed from the list colliders 


        foreach(Collider c in _colliders) //For each collider check if it has an entity script and remove if it does
        {
            if(c.gameObject.GetComponent<Entity>() != null && _ani.GetCurrentAnimatorStateInfo(0).IsTag("attack")) // Also check if animation is playing
            {
                Entity healthScript = c.gameObject.GetComponent<Entity>(); // Call entity script of the hit entity
                int damage = 20; //Amount of damage
                healthScript.LoseHealth(damage); // Call the LoseHealth function from entity script
                toRemove.Add(c); //Add collider in a list to remove it, so it can be removed after the for each loop
            }
        }

        foreach(Collider c in toRemove)
        {
            _colliders.Remove(c); //Remove from collider list
        }

    }
    private void OnTriggerEnter(Collider _collision)
    {
        if (!_colliders.Contains(_collision))
            _colliders.Add(_collision); //Add collider in list if the weapon hits something

    }

    private void OnTriggerExit(Collider _collision)
    {
        if (_colliders.Contains(_collision))
            _colliders.Remove(_collision); //Remove collider when its not hitting stuff anymore.
    }
}

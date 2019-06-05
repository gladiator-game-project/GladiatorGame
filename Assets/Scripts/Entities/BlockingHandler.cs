using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlockingHandler : MonoBehaviour
{

    /// <summary>
    /// This method checks if the weapon has collided with armor. If so, the isImmune variable is set to true, so the entity does not receive damage.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Armor") || other.CompareTag("Shield") || other.CompareTag("Weapon"))
        {
            var damageHandler = other.GetComponentInParent<DamageHandler>();
            if (damageHandler != null)
                damageHandler.IsImmune = true;
            
        }
    }
}

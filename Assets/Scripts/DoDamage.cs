﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamage : MonoBehaviour
{
    void Start()
    {

    }

    private void OnTriggerEnter(Collider _collision)
    {
        if (_collision.gameObject.GetComponent<Entity>() != null)
        {
            Entity healthScript = _collision.gameObject.GetComponent<Entity>();
            int damage = 20;
            healthScript.LoseHealth(damage);
        }
    }
}

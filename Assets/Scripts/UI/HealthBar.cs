using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Entity _entityscript;
    private Image _imgComponent;

    void Start()
    {
        _entityscript = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>();
        _imgComponent = GetComponent<Image>();
    }

    void Update()
    {
        SizeHealthBar(); // keep checking the size
    }

    public void SizeHealthBar() // function to check the healthbar and resize it if necesary
    {
        float currentHealth = _entityscript.Health; // get currenthealth of entitiy
        float maxHealth = _entityscript.maxHealth; // get maxhealth of entity
        float promille = currentHealth / maxHealth; // calculate promille of the remaining player health
        float fillAmount = 1 - promille; // filling the promille has to be reversed.
        _imgComponent.fillAmount = fillAmount;
    }
}

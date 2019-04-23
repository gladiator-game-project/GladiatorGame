using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject Entity;
    private RectTransform Transform;
    private Entity Entityscript;
    private Image imgComponent;

    // Start is called before the first frame update
    void Start()
    {
        Transform = GetComponent<RectTransform>();
        Entityscript = Entity.GetComponent<Entity>();
        imgComponent = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        SizeHealthBar(); // keep checking the size
    }

    public void SizeHealthBar() // function to check the healthbar and resize it if necesary
    {
        float currentHealth = Entityscript.Health; // get currenthealth of entitiy
        float maxHealth = Entityscript.maxHealth; // get maxhealth of entity
        float promille = currentHealth / maxHealth; // calculate promille of the remaining player health
        float fillAmount = 1 - promille; // filling the promille has to be reversed.
        imgComponent.fillAmount = fillAmount;
    }
}

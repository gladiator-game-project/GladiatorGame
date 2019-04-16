using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject Entity;
    private RectTransform Transform;
    private Entity Entityscript;
    // Start is called before the first frame update
    void Start()
    {
        Transform = GetComponent<RectTransform>();
        Entityscript = Entity.GetComponent<Entity>();
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
        float percentage = currentHealth / maxHealth * 100F; // calculate percentage of health the entity has
        float width = percentage / 100 * 200; // calculate width, 200 is normal width
        Transform.sizeDelta = new Vector2(width,Transform.sizeDelta.y); // set height and width
    }
}

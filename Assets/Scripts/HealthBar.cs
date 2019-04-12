using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject Entity;
    private RectTransform Transform;
    // Start is called before the first frame update
    void Start()
    {
        Transform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        SizeHealthBar(); // keep checking the size
    }

    public void SizeHealthBar() // function to check the healthbar and resize it if necesary
    {
        int currentHealth = Entity.GetComponent<Entity>().Health; // get currenthealth of entitiy
        int maxHealth = Entity.GetComponent<Entity>().maxHealth; // get maxhealth of entity
        float percentage = currentHealth / maxHealth * 100; // calculate percentage of health the entity has
        float width = percentage / 100 * 200; // calculate width, 200 is normal width
        Transform.sizeDelta = new Vector2(width,30); // height is set so it stays 30
    }
}

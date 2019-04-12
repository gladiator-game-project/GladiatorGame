using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBar : MonoBehaviour
{
    public GameObject Entity;
    private RectTransform Transform;
    private Entity EntityScript;
    // Start is called before the first frame update
    void Start()
    {
        Transform = GetComponent<RectTransform>();
        EntityScript = Entity.GetComponent<Entity>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckWidth();
    }

    private void CheckWidth()
    {
        int currentStamina = EntityScript.Stamina;
        int maxStamina = EntityScript.maxStamina;
        float percentage = currentStamina / maxStamina * 100;
        float width = percentage / 100 * 200;
        Transform.sizeDelta = new Vector2(width,Transform.sizeDelta.y);
    }
}

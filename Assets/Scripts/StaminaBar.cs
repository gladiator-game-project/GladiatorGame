using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public GameObject Entity;
    private RectTransform Transform;
    private Entity EntityScript;
    private Image imgComponent;

    // Start is called before the first frame update
    void Start()
    {
        Transform = GetComponent<RectTransform>();
        EntityScript = Entity.GetComponent<Entity>();
        imgComponent = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckWidth();
    }

    private void CheckWidth()
    {
        float currentStamina = EntityScript.Stamina;
        float maxStamina = EntityScript.maxStamina;
        float promille = currentStamina / maxStamina;
        float fillAmount = 1 - promille;
        imgComponent.fillAmount = fillAmount;
    }
}

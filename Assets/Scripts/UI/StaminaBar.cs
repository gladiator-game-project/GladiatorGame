using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    private Entity _entityScript;
    private Image _imgComponent;

    void Start()
    {
        _entityScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>();
        _imgComponent = GetComponent<Image>();
    }

    void Update()
    {
        CheckWidth();
    }

    private void CheckWidth()
    {
        float currentStamina = _entityScript.Stamina;
        float maxStamina = _entityScript.maxStamina;
        float promille = currentStamina / maxStamina;
        float fillAmount = 1 - promille;
        _imgComponent.fillAmount = fillAmount;
    }
}

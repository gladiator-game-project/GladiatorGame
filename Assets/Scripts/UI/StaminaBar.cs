using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Entities;

namespace Assets.Scripts.UI
{
    public class StaminaBar : MonoBehaviour
    {
        private Entity _entityScript;
        private Image _imgComponent;

        void Start()
        {
            _entityScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Entities.Entity>();
            _imgComponent = GetComponent<Image>();
        }

        void Update()
        {
            CheckWidth();
        }

        private void CheckWidth()
        {
            float currentStamina = _entityScript.Stamina;
            float maxStamina = _entityScript.MaxStamina;
            float promille = currentStamina / maxStamina;
            float fillAmount = 1 - promille;
            _imgComponent.fillAmount = fillAmount;
        }
    }
}

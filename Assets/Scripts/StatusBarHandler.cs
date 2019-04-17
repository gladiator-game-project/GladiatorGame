using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBarHandler : MonoBehaviour
{
    public Entity player;

    private Image healthBlock;
    private Image staminaBlock;

    // Start is called before the first frame update
    void Start()
    {
        healthBlock = transform.Find("Health Block").GetComponent<Image>();
        staminaBlock = transform.Find("Stamina Block").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBlock.fillAmount = 1 - ((float)player.Health / (float)player.maxHealth);
        staminaBlock.fillAmount = 1 - ((float)player.Stamina / (float)player.maxStamina);
    }
}

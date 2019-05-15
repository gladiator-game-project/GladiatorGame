using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float Coins;
    // Start is called before the first frame update
    void Start()
    {
        Coins = 0.0f; //can change
        //TODO: Playerprefs to save
    }

    public void AddCoins(float money)
    {
        Coins += money;
    }

    public void LoseCoins(float money)
    {
        if ((Coins - money) < 0)
            Coins = 0;
        else
            Coins -= money;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float Coins;
    // Start is called before the first frame update
    void Start()
    {
        //TODO: Add playerprefs to save coins
        Coins = 0.0f; //can change
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void AddMoney(float money)
    {
        Coins += money;
    }

    public void LoseMoney(float money)
    {
        if ((Coins - money) < 0)
            return;
        else
            Coins -= money;
    }
}

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float Coins;
    // Start is called before the first frame update
    private void Start()
    {
        //TODO: Playerprefs to save coins
        Coins = 0.0f; // begin coins
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

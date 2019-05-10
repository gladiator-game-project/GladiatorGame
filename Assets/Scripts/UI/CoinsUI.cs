using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsUI : MonoBehaviour
{
    private PlayerStats _playerStats;
    private Text _coinText;

    // Start is called before the first frame update
    void Start()
    {
        _playerStats = GameObject.Find("GameManager").GetComponent<PlayerStats>();
        _coinText = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _coinText.text = _playerStats.Coins.ToString();
    }
}

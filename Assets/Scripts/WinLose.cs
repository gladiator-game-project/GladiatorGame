﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLose : MonoBehaviour
{
    private GameObject _player;
    private Entity _entityscript;
    private PlayerMovement _movementscript;
    private TakeWeapon _takeWeaponscript;
    public GameObject YouWinText;
    private Text _youWinLose;
    public GameObject RestartButton;
    private PlayerStats _playerStats;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        _entityscript = _player.GetComponent<Entity>();
        _youWinLose = YouWinText.GetComponent<Text>();
        _movementscript = _player.GetComponent<PlayerMovement>();
        _takeWeaponscript = _player.GetComponent<TakeWeapon>();
        _playerStats = GameObject.Find("GameManager").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGameStatus();
    }

    private void CheckGameStatus()
    {
        if (_entityscript.Alive == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _youWinLose.text = "Game Over";
            _movementscript.enabled = false;
            _takeWeaponscript.enabled = false;
            RestartButton.SetActive(true);
        }
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        bool allDeath = true; // assume all are death
        foreach (GameObject Enemy in Enemies)
        {
            if (Enemy.GetComponent<Entity>().Alive == true)
            {
                allDeath = false;                               // find one alive, then change the bool
                break;
            }
        }

        if (allDeath == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _youWinLose.text = "You win!";
            _movementscript.enabled = false;
            _takeWeaponscript.enabled = false;
            RestartButton.SetActive(true);
            _playerStats.AddMoney(20.0f);
        }
    }
}

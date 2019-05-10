using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLose : MonoBehaviour
{
    private GameObject _player;
    private Entity _entityscript;
    private PlayerMovement _movementscript;
    private TakeWeapon _takeWeaponscript;
    private PlayerStats _playerstats;
    public GameObject YouWinText;
    private Text _youWinLose;
    public GameObject RestartButton;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _entityscript = _player.GetComponent<Entity>();
        _youWinLose = YouWinText.GetComponent<Text>();
        _movementscript = _player.GetComponent<PlayerMovement>();
        _takeWeaponscript = _player.GetComponent<TakeWeapon>();
        _playerstats = GameObject.Find("GameManager").GetComponent<PlayerStats>();
    }
    
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
            _playerstats.AddCoins(20.0f);
            //TODO: MAke system to give this script the correct number of coins
        }
    }
}

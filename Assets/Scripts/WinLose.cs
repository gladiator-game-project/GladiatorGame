using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLose : MonoBehaviour
{
    private GameObject Player;
    private Entity entityscript;
    private PlayerMovement Movementscript;
    public GameObject YouWinText;
    private Text YouWinLose;
    public GameObject RestartButton;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        entityscript = Player.GetComponent<Entity>();
        YouWinLose = YouWinText.GetComponent<Text>();
        Movementscript = Player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGameStatus();
    }

    private void CheckGameStatus()
    {
        if (entityscript.Alive == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            YouWinLose.text = "Game Over";
            Movementscript.enabled = false;
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
            YouWinLose.text = "You win!";
            Movementscript.enabled = false;
            RestartButton.SetActive(true);
        }
    }
}

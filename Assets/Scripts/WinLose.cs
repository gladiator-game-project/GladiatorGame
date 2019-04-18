using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLose : MonoBehaviour
{
    private GameObject Player;
    private Entity entityscript;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        entityscript = Player.GetComponent<Entity>();
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
            Debug.Log("Game Over");
            //TODO make game over screen?
        }
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        bool allDeath = true; // assume all are death
        foreach (GameObject Enemy in Enemies)
        {
            if (Enemy.GetComponent<Entity>().Alive == true)
            {
                allDeath = false;                               // find one alive, then change the bool
            }
        }
        if (allDeath == true)
        {
            Debug.Log("You win!");
            //TODO make a you win screen
            allDeath = false;
        }
    }
}

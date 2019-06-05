using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private GameObject InGamePanel;

    public void Start()
    {
        InGamePanel = GameObject.Find("InGamePanel");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            InGamePanel.SetActive(true);
        }
    }

    public void ExitGame() =>
        Application.Quit();

    public void StartGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }


    public void ExitMatch() =>
        SceneManager.LoadScene(0);


}
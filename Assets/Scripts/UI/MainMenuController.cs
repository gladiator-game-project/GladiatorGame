using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private GameObject InGamePanel;

    public void ExitGame() =>
        Application.Quit();

    public void StartGame() =>
        SceneManager.LoadScene(1);

    public void ExitMatch() =>
        SceneManager.LoadScene(0);


    private void Start()
    {
        InGamePanel = GameObject.Find("InGamePanel");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            InGamePanel.SetActive(true);
        }
    }
}
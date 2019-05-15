﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void ExitGame() =>
        Application.Quit();

    public void StartGame() =>
        SceneManager.LoadScene(1);

    public void ExitMatch() =>
    SceneManager.LoadScene(0);
}

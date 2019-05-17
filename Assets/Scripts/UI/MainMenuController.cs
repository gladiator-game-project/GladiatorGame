using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject InGamePanel;

    public void ExitGame() =>
        Application.Quit();

    public void StartGame() =>
        SceneManager.LoadScene(1);

    public void ExitMatch() =>
        SceneManager.LoadScene(0);

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            InGamePanel.SetActive(true);
        }
    }
}
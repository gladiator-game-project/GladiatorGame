using Assets.Scripts.Entities.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private GameObject InGamePanel;
    private GameObject _player;

    public void Start()
    {
        _player = GameObject.Find("Player");
        InGamePanel = GameObject.Find("InGamePanel");

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
            SetIngameMenu(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            SetIngameMenu(true);
    }

    public void SetIngameMenu(bool visible)
    {
        _player.GetComponent<PlayerMovement>().enabled = !visible;
        Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = visible;
        InGamePanel?.SetActive(visible);
    }

    public void ExitGame() =>
        Application.Quit();

    public void StartGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }


    public void ExitMatch() =>
        SceneManager.LoadScene(0, LoadSceneMode.Single);


}
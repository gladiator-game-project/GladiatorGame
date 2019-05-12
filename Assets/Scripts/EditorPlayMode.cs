using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    public class EditorPlayMode : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                EditorApplication.isPlaying = false;
                Cursor.lockState = CursorLockMode.None;
            }

            if (Input.GetKey(KeyCode.P))
            {
                EditorApplication.isPaused = true;
                Cursor.lockState = CursorLockMode.None;
            }

        }
    }
}

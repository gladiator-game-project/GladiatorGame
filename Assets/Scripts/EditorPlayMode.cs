using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorPlayMode : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            EditorApplication.isPlaying = false;
    }
}

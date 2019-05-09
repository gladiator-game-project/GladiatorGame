using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour, IPointerClickHandler
{
    private float _clickTime;
    private int _clickCount = 0;
    public bool onClick = true;
    public bool onDoubleClick = false;
    private Text BtnText;
    public void Start()
    {
        BtnText = transform.gameObject.GetComponentInChildren<Text>();
    }

    public void OnPointerClick(PointerEventData data)
    {
        // single click
        if (onClick)
            SceneManager.LoadScene(1); // Load scene
    }
}
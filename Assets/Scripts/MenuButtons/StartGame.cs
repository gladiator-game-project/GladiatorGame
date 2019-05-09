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

    public void Update()
    {

    }
    public void OnPointerClick(PointerEventData data)
    {
        // get interval between this click and the previous one (check for double click)
        float interval = data.clickTime - _clickTime;

        // if this is double click, change click count
        if (interval < 0.5 && interval > 0 && _clickCount != 2)
        {
            _clickCount = 2;
        }
        else
        {
            _clickCount = 1;
        }
        // reset click time
        _clickTime = data.clickTime;

        // single click
        if (onClick && _clickCount == 1)
            SceneManager.LoadScene(1); // Load scene

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class ExitGame : MonoBehaviour, IPointerClickHandler
{
    private float clickTime;
    private int clickCount = 0;
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
            Application.Quit();
    }
}

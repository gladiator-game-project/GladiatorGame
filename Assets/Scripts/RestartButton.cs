using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour, IPointerClickHandler
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

    public void Update()
    {

    }
    public void OnPointerClick(PointerEventData data)
    {
        // get interval between this click and the previous one (check for double click)
        float interval = data.clickTime - clickTime;

        // if this is double click, change click count
        if (interval < 0.5 && interval > 0 && clickCount != 2)
        {
            clickCount = 2;
        }
        else
        {
            clickCount = 1;
        }
        // reset click time
        clickTime = data.clickTime;

        // single click
        if (onClick && clickCount == 1)
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }

    }
}

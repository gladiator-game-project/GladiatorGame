using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ShowControls : MonoBehaviour, IPointerClickHandler
{
    private float _clickTime;            // time of last click
    private int _clickCount = 0;         // current click count
    public bool onClick = true;            // is click allowed on button?
    public bool onDoubleClick = false;    // is double-click allowed on button?
    private GameObject ControlsPanel;


    // Use this for initialization
    void Start()
    {
        // Calls the ControlPanel
        ControlsPanel = GameObject.Find("ControlsPanel");
    }

    // Update is called once per frame
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
        // The Control panel is being placed on the screen
        if (onClick && _clickCount == 1)
        {
            //The Control panel is being positioned at a 4x smaller X-as position
            Vector3 p = ControlsPanel.transform.position;
            Debug.Log(p.x);
            p.x = p.x / 4;
            ControlsPanel.transform.position = p;
        }
    }
}

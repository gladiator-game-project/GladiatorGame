using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ShowControls : MonoBehaviour, IPointerClickHandler
{
    private float clickTime;            // time of last click
    private int clickCount = 0;         // current click count
    public bool onClick = true;            // is click allowed on button?
    public bool onDoubleClick = false;    // is double-click allowed on button?
    private GameObject ControlsPanel;


    // Use this for initialization
    void Start()
    {
        // Roept het ShopMenu aan
        ControlsPanel = GameObject.Find("ControlsPanel");
    }

    // Update is called once per frame
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
        // De shop openen en terugplaatsten en verwijdermodus afsluiten wanneer het aanstaat
        if (onClick && clickCount == 1)
        {
            //verplaatst de shop naar een 4x grotere X-as positie
            Vector3 p = ControlsPanel.transform.position;
            Debug.Log(p.x);
            p.x = p.x / 4;
            ControlsPanel.transform.position = p;
        }
    }
}

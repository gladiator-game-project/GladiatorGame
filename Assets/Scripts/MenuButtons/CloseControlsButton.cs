using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class CloseControlsButton : MonoBehaviour, IPointerClickHandler
{
    private GameObject ControlsPanel;

    // Use this for initialization
    void Start()
    {
        // Calls the ControlsPanel
        ControlsPanel = GameObject.Find("ControlsPanel");
    }

    // Update is called once per frame
    public void OnPointerClick(PointerEventData data)
    {
        //The Control panel is being positioned at a 4x larger X-as position.
        Vector3 p = ControlsPanel.transform.position;
        Debug.Log(p.x);
        p.x = p.x * 4;
        ControlsPanel.transform.position = p;
    }
}

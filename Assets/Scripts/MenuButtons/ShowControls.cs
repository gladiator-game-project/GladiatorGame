﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ShowControls : MonoBehaviour, IPointerClickHandler
{
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
        //The Control panel is being positioned at a 4x smaller X-as position
        Vector3 p = ControlsPanel.transform.position;
        Debug.Log(p.x);
        p.x = p.x / 4;
        ControlsPanel.transform.position = p;
    }
}

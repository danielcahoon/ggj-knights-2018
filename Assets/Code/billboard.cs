﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class billboard : MonoBehaviour {

    public float maxDistance = 10.0f;
    public float minDistance = 2.0f;
    public float menuDistance = 3.0f;
    public float scaleFactor = 0.03f; //edit this to control size on screen

    public string menuTitle = "";
    public MenuOption[] menuOptionData;

    public GameObject menuPanel;

    private bool isMenuDisplayed = false;
    private choiceMenu menu;
    private DeviceBehavior device;

	// Use this for initialization
	void Start () {
        menu = menuPanel.GetComponent<choiceMenu>();
        device = (DeviceBehavior)GetComponentInParent(typeof(DeviceBehavior));
    }
	
	// Update is called once per frame
	void Update () {

        Vector3 screenPoint = Camera.main.WorldToViewportPoint(this.transform.parent.position);
        bool isOnScreen =
            screenPoint.z > 0 &&
            screenPoint.x > 0 &&
            screenPoint.x < 1 &&
            screenPoint.y > 0 &&
            screenPoint.y < 1;


        // show/hide billboard based on distance
        var distance = Vector3.Distance(Camera.main.transform.position, this.transform.position);
        if (distance < maxDistance && distance > minDistance && isOnScreen)
        {
            // face sprite at camera
            transform.LookAt(Camera.main.transform.position, Vector3.up);

            //scale billboard to fixed screen size
            float size = (Camera.main.transform.position - transform.position).magnitude;
            transform.localScale = new Vector3(size, size, size) * scaleFactor;
            if (!GetComponent<Renderer>().enabled) {

                GetComponent<Renderer>().enabled = true;
                //Debug.Log("on");
            }
        } else if (GetComponent<Renderer>().enabled)
        {
            GetComponent<Renderer>().enabled = false;
            //Debug.Log("off");
        }

        var shouldShowMenu = false;

        if (distance < menuDistance)
        {
            if (isOnScreen)
            {
                shouldShowMenu = true;
            } else 
            {
                shouldShowMenu = false;
            }
        } else
        {
            shouldShowMenu = false;
        }



        if (shouldShowMenu && !menu.isOpen)
        {
            menu.ActivateMenu(this.getFilteredMenuOptionData(), this.menuTitle);
            menu.activeBillboard = this;
        } else if (!shouldShowMenu && menu.isOpen && menu.activeBillboard == this)
        {
            menu.CloseMenu();
        }

    }

    private MenuOption[] getFilteredMenuOptionData()
    {
        if (device == null)
        {
            return this.menuOptionData;
        }

        return this.menuOptionData.Where(c => {
            return !(c.hideIfInfected && device.isInfected) &&
                !(c.hideIfUninfected && !device.isInfected) &&
                !(c.hideIfOn && device.isOn) &&
                !(c.hideIfOff && !device.isOn);
            }).ToArray();
    }
}
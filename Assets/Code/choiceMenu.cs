using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class choiceMenu : MonoBehaviour {

    public bool isOpen = false;
    private MenuOption[] menuOptionData;
    public Text[] menuOptionList;
    internal billboard activeBillboard;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (menuOptionData != null)
        {
            for (var i = 0; i < menuOptionData.Length && i < menuOptionList.Length; i++)
            {
                if (Input.GetKeyDown(menuOptionData[i].key) && menuOptionData[i].callback != null)
                {
                    menuOptionData[i].callback.Invoke();
                    this.CloseMenu();
                    break;
                }
            }
        }
    }

    public void ActivateMenu(MenuOption[] menuOptionData)
    {
        this.menuOptionData = menuOptionData;
        this.updateMenuText();
        this.gameObject.SetActive(true);
        this.isOpen = true;
    }

    public void CloseMenu()
    {
        menuOptionData = null;
        this.gameObject.SetActive(false);
        this.isOpen = false;
    }

    private void updateMenuText()
    {
        for (var i = 0; i < menuOptionList.Length; i++)
        {
            if (i >= menuOptionData.Length)
            {
                menuOptionList[i].text = "";
            } else
            {
                menuOptionList[i].text = "Press \"" + menuOptionData[i].key + "\" to " + menuOptionData[i].message;
            }
        }
    }
}
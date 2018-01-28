using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class choiceMenu : MonoBehaviour {

    public bool isOpen = false;
    private MenuOption[] menuOptionData;
    private string menuTite;
    public GameObject[] menuOptionList;
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

    public void ActivateMenu(MenuOption[] menuOptionData, string title)
    {
        this.menuOptionData = menuOptionData;
        this.menuTite = title;
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
                menuOptionList[i].SetActive(false);
            } else
            {
                menuOptionList[i].SetActive(true);
                this.transform.Find("Title").GetComponent<Text>().text = this.menuTite;
                menuOptionList[i].transform.Find("OptionBorder/OptionKey").GetComponent<Text>().text = menuOptionData[i].key.ToUpper();
                menuOptionList[i].transform.Find("OptionText").GetComponent<Text>().text = menuOptionData[i].message.ToUpper();
            }
        }

        // this.GetComponent<RectTransform>().Height

        switch(menuOptionData.Length)
        {
            case 1:
                GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 95.25f);
                break;
            case 2:
                GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 135.25f);
                break;
            default:
                GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 176.97f);
                break;
        }
    }
}
﻿using UnityEngine.Events;

[System.Serializable]
public class MenuOption
{
    public string key;
    public string message;
    public UnityEvent callback;
}
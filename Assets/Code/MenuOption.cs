using UnityEngine.Events;

[System.Serializable]
public class MenuOption
{
    public string key;
    public string message;
    public UnityEvent callback;
    public bool hideIfOff = false;
    public bool hideIfOn = false;
    public bool hideIfInfected = false;
    public bool hideIfUninfected = false;
}
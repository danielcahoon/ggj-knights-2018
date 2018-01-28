using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeviceBehavior : MonoBehaviour {

    public bool isInfected = false;
    public bool isOn = true;

    public UnityEvent onPowerOn;
    public UnityEvent onPowerOff;

    public UnityEvent onInfection;
    public UnityEvent onCure;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void toggleOnOff()
    {
        this.isOn = !this.isOn;
        if (this.isOn && this.onPowerOn != null)
        {
            this.onPowerOn.Invoke();
        }
        if (!this.isOn && this.onPowerOff != null)
        {
            this.onPowerOff.Invoke();
        }
    }

    public void setInfected()
    {
        this.setInfectedState(true);
    }

    public void setCured()
    {
        this.setInfectedState(false);
    }

    private void setInfectedState(bool isInfected)
    {
        if (!this.isInfected && isInfected)
        {
            this.isInfected = true;
            if (this.onInfection != null)
            {
                this.onInfection.Invoke();
            }
        }


        if (this.isInfected && !isInfected)
        {
            this.isInfected = false;
            if (this.onCure != null)
            {
                this.onCure.Invoke();
            }
        }
    }
}

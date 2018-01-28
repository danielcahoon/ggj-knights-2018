﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightOff : MonoBehaviour {

    private Light lightReference;
    public float animationDuration = 2.0f;
    private float animationRunTime = 999;


    private Color fadeColor = new Color(0, 0, 0, 255);
    private Color origColor;

    private bool isRestore = false;

	// Use this for initialization
	void Start () {
        lightReference = this.GetComponent<Light>();
    }
	
	// Update is called once per frame
	void Update () {
		if (animationRunTime < animationDuration)
        {
            var startColor = this.isRestore ? this.fadeColor : this.origColor;
            var endColor = this.isRestore ? this.origColor : this.fadeColor;

            float ratioComplete = animationRunTime / animationDuration;
            float ratioInverse = (1 - ratioComplete);
            this.lightReference.color = new Color(
                startColor.r * ratioInverse + endColor.r * ratioComplete,
                startColor.g * ratioInverse + endColor.g * ratioComplete,
                startColor.b * ratioInverse + endColor.b * ratioComplete);

            this.animationRunTime += Time.deltaTime;
        }
	}


    public void Fade()
    {
        this.isRestore = false;
        this.resetTime();
        this.origColor = this.lightReference.color;
    }

    public void Restore()
    {
        this.isRestore = true;
        this.resetTime();
    }

    void resetTime()
    {
        if (this.animationRunTime < this.animationDuration)
        {
            this.animationRunTime = this.animationDuration - this.animationRunTime;
        } else
        {
            this.animationRunTime = 0;
        }
    }
}

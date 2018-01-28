using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoombaInfector : MonoBehaviour {

    public Doomba target;
    public float safeTime = 15.0f;

    private float infectTime = -1.0f;
    private DeviceBehavior device;

    // Use this for initialization
    void Start () {
        infectTime = Time.time + safeTime;
        if(target == null)
        {
            Debug.Log("WARNING: DOOMBA SPECIFIED FOR INFECTOR IS NULL. WILL NOT ACTIVATE.");
            Destroy(this);
        }
        device = target.GetComponent<DeviceBehavior>();
        if (device == null)
        {
            Debug.Log("WARNING: DOOMBA SPECIFIED FOR INFECTOR HAS NO DEVICE BEHAVIOR. WILL NOT ACTIVATE.");
            Destroy(this);
        }
	}

    private bool targetIsVisible()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(target.transform.position);
        return screenPoint.z > 0 &&
            screenPoint.x > 0 &&
            screenPoint.x < 1 &&
            screenPoint.y > 0 &&
            screenPoint.y < 1;
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > infectTime && !targetIsVisible())
        {
            target.infected = true;
            device.setInfected();
            Destroy(this);
        }
	}
}

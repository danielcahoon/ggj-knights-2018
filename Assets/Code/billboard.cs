using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboard : MonoBehaviour {

    public float maxDistance = 10.0f;
    public float minDistance = 2.0f;
    public float scaleFactor = 0.03f; //edit this to control size on screen

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // show/hide billboard based on distance
        var distance = Vector3.Distance(Camera.main.transform.position, this.transform.position);
        if (distance < maxDistance && distance > minDistance)
        {
            // face sprite at camera
            transform.LookAt(Camera.main.transform.position, Vector3.up);

            //scale billboard to fixed screen size
            float size = (Camera.main.transform.position - transform.position).magnitude;
            transform.localScale = new Vector3(size, size, size) * scaleFactor;
            GetComponent<Renderer>().enabled = true;
        } else
        {
            GetComponent<Renderer>().enabled = false;
        }

    }
}
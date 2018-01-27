using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update()
    {
        if (Input.GetMouseButton(1)) //strafing
        {
            Cursor.visible = false;
            var x = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f * this.transform.lossyScale.magnitude;
            var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f * this.transform.lossyScale.magnitude;

            transform.Translate(x, 0, 0);
            transform.Translate(0, 0, z);
        }
        else
        {

            Cursor.visible = true;
            var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
            var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f * this.transform.lossyScale.magnitude;

            transform.Rotate(0, x, 0);
            transform.Translate(0, 0, z);
        }
    }
}
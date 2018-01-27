using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testPlayerController : MonoBehaviour {
	// Mouse Look
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	public float minimumX = -360F;
	public float maximumX = 360F;
	public float minimumY = -60F;
	public float maximumY = 60F;
	float rotationX = 0F;
	float rotationY = 0F;
	Quaternion originalRotation;

	// Health/Death
	public int health;
	public string mainMenu;

    private bool grabMouse = true;
	// Use this for initialization
	void Start () {
		originalRotation = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update()
    {
        readInput();

        deathCheck ();
		//Cursor.visible = false;
        if (grabMouse)
        {
            mouseLook();
        }
        //Cursor.visible = false;

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f * this.transform.lossyScale.magnitude;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f * this.transform.lossyScale.magnitude;

        transform.Translate(x, 0, 0);
        transform.Translate(0, 0, z);
	}

    private void readInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            grabMouse = false;
        }

        if (Input.GetMouseButton(0))
        {
            grabMouse = true;
        }
    }

    void mouseLook() {
		Cursor.lockState = CursorLockMode.Locked;
		if (axes == RotationAxes.MouseXAndY)
		{
			// Read the mouse input axis
			rotationX += Input.GetAxis("Mouse X") * sensitivityX;
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationX = ClampAngle (rotationX, minimumX, maximumX);
			rotationY = ClampAngle (rotationY, minimumY, maximumY);
			Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
			Quaternion yQuaternion = Quaternion.AngleAxis (rotationY, -Vector3.right);
			transform.localRotation = originalRotation * xQuaternion * yQuaternion;
		}
		else if (axes == RotationAxes.MouseX)
		{
			rotationX += Input.GetAxis("Mouse X") * sensitivityX;
			rotationX = ClampAngle (rotationX, minimumX, maximumX);
			Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
			transform.localRotation = originalRotation * xQuaternion;
		}
		else
		{
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = ClampAngle (rotationY, minimumY, maximumY);
			Quaternion yQuaternion = Quaternion.AngleAxis (-rotationY, Vector3.right);
			transform.localRotation = originalRotation * yQuaternion;
		}
	}
	public static float ClampAngle (float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp (angle, min, max);
	}

	public void deathCheck() {
		if (health <= 0 && mainMenu != "") { 
			SceneManager.LoadScene(mainMenu);
		}
	}
}

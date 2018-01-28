using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

public class Doomba : MonoBehaviour {

	// permanent properties (the Doomba NEVER gives up on a target)
	public float moveSpeed = 1.0f;
	public float turnSpeed = 90.0f;
	public float diversionAngle = 20.0f;
	public float avoidanceDistance = 0.5f;
	public float backupDistance = 0.5f;
    private float initialHeight = 0.0f;

    // keep track of information about current state
    // as well as when to stop that state
    public bool infected = false;
	public string state = "bumbling";
	public float endTime = -1.0f;

	public FirstPersonController player;

	// DARK MAGIC. DO NOT TOUCH.
	private float intendedRotation = 0.0f;

	// Use this for initialization
	void Start () {
		enterBaseState ();
		initialHeight = transform.position.y;
		intendedRotation = transform.eulerAngles.y;
	}
	
	// Update is called once per frame
	void Update () {
		if(player == null) return;

		switch (state) {
		case "bumbling":
			bumble ();
			break;
		case "seeking":
			seek ();
			break;
		case "backingUp":
			backUp ();
			break;
		case "diverting":
			divert ();
			break;
		case "avoiding":
			avoid ();
			break;
		default:
			left ();
			Debug.Log("OH NO!! THE DOOMBA STATE IS INVALID!! '" + state.ToString() + "'");
			break;
		}

		transform.rotation = Quaternion.Euler (new Vector3(0.0f, intendedRotation, 0.0f));
	}

	void enterBaseState() {
		state = infected ? "seeking" : "bumbling";
	}

	void OnCollisionEnter(Collision collision) {
		// ignore floors. they don't exist :3
		if (collision.gameObject.tag == "floor")
			return;
		else if (collision.gameObject.tag == "Player")
			player.takeDamage (20);

		// switch to the "backingUp" state, for the specified amount of time
		endTime = Time.time + (backupDistance / moveSpeed);
		state = "backingUp";
	}

	void bumble() {
		forward ();
	}

	void seek() {
		forward ();

		var targetVec = player.transform.position - transform.position;
		var rightVec = transform.right;
		if (Vector3.Dot (targetVec, rightVec) < 0.0f) {
			left ();
		} else {
			right ();
		}
	}

	void backUp() {
		backward ();

		if (Time.time > endTime) {
			state = "diverting";
			endTime = Time.time + (diversionAngle / turnSpeed);
		}
	}

	void divert() {
		left ();

		if (Time.time > endTime) {
			state = "avoiding";
			endTime = Time.time + (avoidanceDistance / moveSpeed);
		}
	}

	void avoid() {
		forward ();

		if (Time.time > endTime)
			enterBaseState ();
	}

	void forward() {
		transform.position += transform.forward * moveSpeed * Time.deltaTime;
		transform.position = new Vector3(transform.position.x, initialHeight, transform.position.z);
	}

	void backward() {
		transform.position -= transform.forward * moveSpeed * Time.deltaTime;
		transform.position = new Vector3(transform.position.x, initialHeight, transform.position.z);
	}

	void left() {
		intendedRotation -= turnSpeed * Time.deltaTime;
	}

	void right() {
		intendedRotation += turnSpeed * Time.deltaTime;
	}
}

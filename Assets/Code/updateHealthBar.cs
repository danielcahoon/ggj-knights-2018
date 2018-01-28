using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updateHealthBar : MonoBehaviour {
	public testPlayerController player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		RectTransform rt = GetComponent (typeof (RectTransform)) as RectTransform;
		rt.sizeDelta = new Vector2 (5 * player.health, 13);
	}
}

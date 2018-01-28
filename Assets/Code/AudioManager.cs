using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	public AudioSource goodMorning;
	public AudioSource taunt;
	public AudioSource music;
	public bool gmPlayed = false;
	public bool tauntPlayed = false;
	public bool musicPlayed = false;
	public bool skip = false;
	// Use this for initialization
	void Start () {
		music.loop = true;
	}

	// Update is called once per frame
	void Update () {
		if (!skip &&  Input.GetKeyDown (KeyCode.Q)) {
			skip = true;
			goodMorning.Stop ();
			taunt.Stop ();
 			music.Play ();
		}
		if (!skip && !gmPlayed && !goodMorning.isPlaying) {
			goodMorning.Play ();
			gmPlayed = true;
		}
		if (!skip && !tauntPlayed && !taunt.isPlaying && !goodMorning.isPlaying && gmPlayed) {
			taunt.Play ();
			tauntPlayed = true;
		}
		if (!skip && !musicPlayed && !music.isPlaying && !taunt.isPlaying && !goodMorning.isPlaying  && gmPlayed && tauntPlayed) {
			music.Play ();
			musicPlayed = true;
		}
	}
}

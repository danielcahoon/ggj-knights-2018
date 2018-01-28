using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoombaInfector : MonoBehaviour {

    public Doomba target;
    public float safeTime = 15.0f;

    private float infectTime = -1.0f;
    private Renderer rend;

    // Use this for initialization
    void Start () {
        infectTime = Time.time + safeTime;
        if(target == null)
        {
            Debug.Log("WARNING: DOOMBA SPECIFIED FOR INFECTOR IS NULL. WILL NOT ACTIVATE.");
            Destroy(this);
        }
        rend = target.GetComponent<Renderer>();
        if (rend == null)
        {
            Debug.Log("WARNING: DOOMBA SPECIFIED FOR INFECTOR HAS NO RENDERER. WILL NOT ACTIVATE.");
            Destroy(this);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > infectTime && !rend.isVisible)
        {
            target.infected = true;
            Destroy(this);
        }
	}
}

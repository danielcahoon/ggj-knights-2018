using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winCollider : MonoBehaviour {
    public string sceneName = "win";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        SceneManager.LoadScene(this.sceneName);
    }
}

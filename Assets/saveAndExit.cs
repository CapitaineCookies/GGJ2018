using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class saveAndExit : MonoBehaviour {

	public carnet car;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseUp() {
		Debug.Log ("mouse exit up");
		car.saveToPng ();
		SceneManager.LoadScene ("carnet");
	}

}

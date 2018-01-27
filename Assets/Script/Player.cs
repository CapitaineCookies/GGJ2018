using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float health;
	public float experience;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Save(){
		SaveLoadManager.Save (this);
	}

	public void Load() {
		SequenceValidatorData data = SaveLoadManager.LoadPlayer ();
		//health = data.health;
		//experience = data.experience;

		GetComponent<PlayerDisplay>().UpdateDisplay ();
	}
 


}

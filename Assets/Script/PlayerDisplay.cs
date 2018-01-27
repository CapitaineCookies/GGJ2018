using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour {

	public Text health;
	public Text experience;
	public Player player;


	void Awake() {
		player = GetComponent<Player> ();
	}

	// Use this for initialization
	void Start () {
		UpdateDisplay ();
	}
	 

	public void UpdateDisplay() {
		health.text = player.health.ToString ();
		experience.text = player.experience.ToString ();
	}
}

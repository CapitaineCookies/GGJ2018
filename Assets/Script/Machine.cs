using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Machine : MonoBehaviour {

	public AudioClip stressMusic;

	private AudioSource audioSource;
	private float duration;

	void Awake() {
		audioSource = GetComponent<AudioSource> ();
		duration = audioSource.clip.length;

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeSinceLevelLoad >= duration) {
			if (checkVictory ()) {
				SceneManager.LoadScene ("victory");
			} else {
				SceneManager.LoadScene ("carnetDessin");
			}
		}
	}

	bool checkVictory() {
		Validable[] validables = GetComponentsInChildren<Validable> ();
		foreach(Validable validable in validables) {
			if (!validable.IsValid()) {
				return false;
			}
		}
		return true;
	}

}

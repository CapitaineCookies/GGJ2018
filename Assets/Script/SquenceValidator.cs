using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquenceValidator : Validable {

	public PushedButton[] buttons;
	// Nombre d'element dans la combinaison
	public int combinaisonSize = 5;

	public int[] sequenceToDo;
	public int current = 0;

	public List<SpriteRenderer> outputFeedBack;
	public Color idleColor = new Color(138f/255f, 194f/255f, 208.0f/255.0f);
	public Color trueColor = Color.green;
	public Color falseColor = Color.red;
	public Color completeColor = Color.green;
	public float feedbackDuration = 0.8f;

	void Awake() {
		Debug.Log ("SquenceValidator Awake");
		buttons = GetComponentsInChildren<PushedButton> ();
		int size = buttons.Length;
		sequenceToDo = new int[combinaisonSize];
		for (int i = 0; i < combinaisonSize; i++) {
			sequenceToDo[i] = Random.Range(0, buttons.Length);
		}
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override bool IsValid() {
		return IsComplete ();
	}

	/**
	 * Valide le boutton. 
	 * Si c'est le bon, on passe on passe au boutton suivant
	 * Si c'etait le dernier, le groupe est valide
	 * Si c'est le mauvais, reinicialise la secance
	 **/
	public bool PushButton(GameObject input) {
		bool good = PushButtonIntern (input);
		foreach(SpriteRenderer output in outputFeedBack) {
			FeedBack (output, good);
		}
		return good;
	}

	public bool PushButtonIntern(GameObject input) {
		if(IsComplete()) {
			return false;
		}
		if (input.Equals (GetNextButton ())) {
			current++;
			return true;
		} else {
			current = 0;
			return false;
		}
	}

	// Si true : le groupe est valide
	public bool IsComplete() {
		return current >= sequenceToDo.Length;
	}

	// Retourne le prochain boutton a valider
	GameObject GetNextButton() {
		return buttons[sequenceToDo[current]].gameObject;
	}

	void FeedBack(SpriteRenderer output, bool good) {
		if(IsComplete()) {
			output.color = completeColor;
			return;
		}
		Color before = idleColor;
		if (good) {
			output.color = trueColor;
		} else {
			output.color = falseColor;
		}
		StartCoroutine (MachineUtil.resetColor(output.gameObject, before, feedbackDuration));
	}

}

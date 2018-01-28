using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquenceValidator : Validable {

	public PushedButton[] buttons;
	// Nombre d'element dans la combinaison
	public int combinaisonSize = 5;

	public int[] sequenceToDo;
	public int current = 0;

	public Feedback[] outputFeedBack;
	public float feedbackDuration = 0.8f;

	void Awake() {
		Debug.Log ("SquenceValidator Awake");
		buttons = GetComponentsInChildren<PushedButton> ();
		int size = buttons.Length;
		sequenceToDo = new int[combinaisonSize];
		for (int i = 0; i < combinaisonSize; i++) {
			sequenceToDo[i] = Random.Range(0, buttons.Length);
		}
		outputFeedBack = GetComponentsInChildren<Feedback> ();
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
		if(IsComplete()) {
			return false;
		}
		bool good = PushButtonIntern (input);
		foreach(Feedback output in outputFeedBack) {
			FeedBack (output, good);
		}
		return good;
	}

	public bool PushButtonIntern(GameObject input) {
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

	void FeedBack(Feedback output, bool good) {
		lastClickId++;
		if(IsComplete()) {
			output.changeState(FeedbackState.COMPLETE);
			return;
		}
		if (good) {
			output.changeState(FeedbackState.TRUE);
		} else {
			output.changeState(FeedbackState.FALSE);
		}
		StartCoroutine (resetColor(output, feedbackDuration, lastClickId));
	}

	private int lastClickId;

	public IEnumerator resetColor (Feedback feedback, float duration, int clickId)
	{
		yield return new WaitForSeconds (duration);
		if (clickId == lastClickId) {
			feedback.changeState (FeedbackState.DEFAULT);
		}
	}

}

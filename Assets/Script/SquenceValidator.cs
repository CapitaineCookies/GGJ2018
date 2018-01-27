using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquenceValidator : MonoBehaviour {

	public List<GameObject> buttons;
	// Nombre d'element dans la combinaison
	public int combinaisonSize = 3;

	public List<int> orders;
	public int current = 0;

	public List<SpriteRenderer> outputFeedBack;
	public Color idleColor = new Color(138f/255f, 194f/255f, 208.0f/255.0f);
	public Color trueColor = Color.green;
	public Color falseColor = Color.red;
	public Color completeColor = Color.green;
	public float feedbackDuration = 0.8f;

	// Use this for initialization
	void Start () {
		int size = buttons.Count;
		orders = new List<int>();
		for (int i = 0; i < combinaisonSize; i++) {
			orders.Add (Random.Range(0, buttons.Count));
		}
	}
	
	// Update is called once per frame
	void Update () {
		
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
		return current >= orders.Count;
	}

	// Retourne le prochain boutton a valider
	GameObject GetNextButton() {
		return buttons[orders[current]];
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

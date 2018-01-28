using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButtonGroup : Validable {

	public SwitchButton[] switchButtons;
	private Feedback[] feedbacks; 

	public bool[] statesNeed;

	public void Awake() {
		feedbacks = GetComponentsInChildren<Feedback> ();
		switchButtons = GetComponentsInChildren<SwitchButton> ();
		statesNeed = new bool[switchButtons.Length];
		for (int i = 0; i < switchButtons.Length; i++) {
			statesNeed[i] = Random.Range (0, 2) == 1;
		}
	}

	public void Update() {
		FeedbackState state = IsValid () ? FeedbackState.COMPLETE : FeedbackState.DEFAULT;
		foreach(Feedback feedback in feedbacks) {
			feedback.changeState (state);
		}
	}


	public override bool IsValid() {
		for (int i = 0; i < switchButtons.Length; i++) {
			bool stateActual = switchButtons[i].isOn;
			bool stateNeed = statesNeed [i];
			if(stateActual != stateNeed) {
				return false;
			}
		}
		return true;
	}


}

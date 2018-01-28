using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelecterGrpValidator : Validable {

	public Selecter[] selecters;
	public int[] targetPositions;
	public bool isValid;

	public SpriteRenderer[] feedbackRenderer;

	void Awake() {
		Debug.Log ("SelecterGrpValidator Awake");
		selecters = GetComponentsInChildren<Selecter> ();
		feedbackRenderer = GetFeedbackRenderer ();

	}

	// Use this for initialization
	void Start () {
		targetPositions = new int[selecters.Length];
		for (int i = 0; i < selecters.Length; i++) {
			Selecter selecter = selecters [i];
			int selecterSize = selecter.Count ();
			targetPositions [i] = Random.Range (0, selecterSize);
		}
	}
	
	// Update is called once per frame
	void Update () {
		isValid = CheckSelectionValidity ();
		SetFeedback (isValid);
	}

	void SetFeedback(bool isValid) {
		Color color = isValid ? Color.green : Color.white;
		foreach(SpriteRenderer c in feedbackRenderer) {
			c.color = color;
		}
	}

	bool CheckSelectionValidity() {
		for (int i = 0; i < targetPositions.Length; i++) {
			int valueSelect = selecters [i].selectedPosition;
			int valueNeed = targetPositions [i];
			if (valueSelect != valueNeed) {
				return false;
			}
		}
		return true;
	}

	SpriteRenderer[] GetFeedbackRenderer() {
		SpriteRenderer[] list = GetComponentsInChildren<SpriteRenderer> ();
		List<SpriteRenderer> available = new List<SpriteRenderer>();

		foreach(SpriteRenderer c in list)
		{
			if (c.tag == "Feedback") {
				available.Add (c);
			}
		}

		return available.ToArray();
	}

	public override bool IsValid() {
		return CheckSelectionValidity ();
	}
}

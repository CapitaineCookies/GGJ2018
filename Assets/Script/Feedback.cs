using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feedback : MonoBehaviour {

	private SpriteRenderer spriteRenderer;

	public Color defaultColor = Color.white;
	public Color trueColor = Color.green;
	public Color falseColor = Color.red;
	public Color completeColor = Color.green;

	public void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void changeState(FeedbackState state) {
		spriteRenderer.color = getColor (state);
	}

	private Color getColor(FeedbackState state) {
		switch (state) {
		case FeedbackState.DEFAULT:
			return defaultColor;
		case FeedbackState.FALSE:
			return falseColor;
		case FeedbackState.TRUE:
			return trueColor;
		case FeedbackState.COMPLETE:
			return completeColor;
		}
		return defaultColor;
	}


}
public enum FeedbackState { DEFAULT, FALSE, TRUE, COMPLETE };
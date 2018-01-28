using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnipolarSwitchRenderer : MonoBehaviour {

	public Sprite switchRelease;
	public Sprite switchPushed;

	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeSwitch(UnipolarSwitchState state) {
		switch (state) {
		case UnipolarSwitchState.RELEASE: 
			spriteRenderer.sprite = switchRelease;
			break;
		case UnipolarSwitchState.PUSHED: 
			spriteRenderer.sprite = switchPushed;
			break;
		}
	}
}

public enum UnipolarSwitchState { RELEASE, PUSHED }

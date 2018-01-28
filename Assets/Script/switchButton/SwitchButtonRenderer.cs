using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButtonRenderer : MonoBehaviour {

	public Sprite spriteOn;
	public Sprite spriteOff;
	SpriteRenderer spriteRenderer;

	public void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer> ();

	}

	public void switchButton(bool value) {
		spriteRenderer.sprite = value ? spriteOn : spriteOff;
	}
}

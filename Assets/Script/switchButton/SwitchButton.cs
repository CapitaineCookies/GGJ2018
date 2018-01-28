using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButton : MonoBehaviour {

	public bool isOn;
	SwitchButtonRenderer switchRenderer;

	void Awake() {
		switchRenderer = GetComponent<SwitchButtonRenderer> ();
	}

	void OnMouseDown() {
		isOn = !isOn;
		switchRenderer.switchButton (isOn);
	}


}

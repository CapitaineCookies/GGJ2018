using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushedButton : MonoBehaviour
{
	private UnipolarSwitchRenderer switchRenderer;
	private SquenceValidator squenceValidator;

	public Color feedBackColor = Color.grey;
	public float feedbackDuration = 0.4f;

	void Awake() {
		squenceValidator = GetComponentInParent<SquenceValidator> ();
		switchRenderer = GetComponent<UnipolarSwitchRenderer> ();
	}

	void OnMouseUp ()
	{
		switchRenderer.ChangeSwitch (UnipolarSwitchState.RELEASE);
		squenceValidator.PushButton (gameObject);
	}

	void OnMouseDown() {
		switchRenderer.ChangeSwitch (UnipolarSwitchState.PUSHED);
	}


}

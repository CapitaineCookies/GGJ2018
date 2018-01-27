using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushedButton : MonoBehaviour
{

	public bool lockButton = false;
	public Color feedBackColor = Color.grey;
	public float feedbackDuration = 0.4f;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}


	void OnMouseUpAsButton ()
	{
		if (lockButton) {
			return;
		}
		lockButton = true;
		SquenceValidator squenceValidator = GetComponentInParent<SquenceValidator> ();
		if (squenceValidator.IsComplete ()) {
			return;
		}
		SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
		Color color = renderer.color;
		squenceValidator.PushButton (this.gameObject);
		renderer.color = feedBackColor;
		StartCoroutine (MachineUtil.resetColor(this.gameObject, color, feedbackDuration));
	}


}

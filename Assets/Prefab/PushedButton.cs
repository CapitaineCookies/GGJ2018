using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushedButton : MonoBehaviour
{

	private bool lockButton = false;

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
		if (squenceValidator.PushButton (this.gameObject)) {
			renderer.color = Color.green;
		} else {
			renderer.color = Color.red;
		}
		StartCoroutine (resetColor(color));

	}

	IEnumerator resetColor (Color color)
	{
		yield return new WaitForSeconds (1.0f);
		GetComponent<SpriteRenderer> ().color = color;
		lockButton = false;
	}
}

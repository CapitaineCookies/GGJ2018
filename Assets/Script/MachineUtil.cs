using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineUtil {

	public static IEnumerator resetColor (GameObject source, Color color, float duration)
	{
		
		yield return new WaitForSeconds (duration);
		source.GetComponent<SpriteRenderer> ().color = color;

		// Comportement si c'est un bouton
		PushedButton button = source.GetComponent<PushedButton>();
		if (button != null) {
			button.lockButton = false;
		}
	}
}

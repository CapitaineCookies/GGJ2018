using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selecter : MonoBehaviour {

	public int selectedPosition;

	private SelecterRenderer selecterRenderer;

	void Awake() {
		selecterRenderer = GetComponent<SelecterRenderer> ();
	}

	void OnMouseUpAsButton ()
	{
		incSelectedPosition ();
	}

	private void incSelectedPosition() {
		selectedPosition = (selectedPosition + 1) % Count();
	}

	public int Count () {
		return selecterRenderer.Count ();
	}

}

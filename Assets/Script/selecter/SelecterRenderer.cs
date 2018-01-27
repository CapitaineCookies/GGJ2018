using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelecterRenderer : MonoBehaviour {

	public List<Sprite> sprites;
	private SpriteRenderer spritRenderer;
	private Selecter selecter;

	public void Awake() {
		spritRenderer = GetComponent<SpriteRenderer>();
		selecter = GetComponent<Selecter>();
	}

	public void Update() {
		spritRenderer.sprite = sprites [selecter.selectedPosition];
	}

	public int Count () {
		return sprites.Count;
	}

}

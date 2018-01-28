using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decor : Validable {
	public Sprite[] sprites;
	private SpriteRenderer spr;

	// Use this for initialization
	void Start () {
		spr = GetComponent<SpriteRenderer> ();
		spr.sprite = sprites [(int)Random.Range (0, sprites.Length)];
	}

	// Update is called once per frame
	void Update () {

	}

	public override bool IsValid() {
		return true;
	}

}

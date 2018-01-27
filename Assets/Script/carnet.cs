using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class carnet : MonoBehaviour {

	private Texture2D texture;
	public int width;
	public int height;
	public int offsetX;
	public int offsetY;
	public bool debug;
	public int rayon;
	private bool isDown;
	[SerializeField] private Color colorInk;

	// Use this for initialization
	void Start () {
		isDown=false;
		//colorInk = Color.green;

		texture = new Texture2D(width, height);
		//GetComponent<Renderer>().material.mainTexture = texture;
		if (debug) {
			Color colorR = Color.red;

			for (int y = 0; y < texture.height; y++) {
				for (int x = 0; x < texture.width; x++) {
					
					texture.SetPixel (x, y, colorR);
				}
			}
			texture.Apply ();
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI() {
		Graphics.DrawTexture(new Rect(offsetX, offsetY, width, height), texture);
	}

	void OnMouseDown() {
		isDown = true;
		Debug.Log ("mouse down");
	}

	void OnMouseUp() {
		isDown = false;
	}

	void OnMouseExit() {
		isDown = false;
	}

	void OnMouseOver() {
		Vector3 mpos = Input.mousePosition;
		if (isDown) {
			drawPoint ((int)mpos.x-offsetX,(int)mpos.y-offsetY);
		}
	}

	void drawPoint (int _x, int _y) {
		//Debug.LogFormat("draw {0} - {1}", x, y);
		for (int y = _y-rayon; y < _y+rayon; y++) {
			for (int x = _x-rayon; x < _x+rayon; x++) {
				if(x<width && y<height && x>0 && y>0 && Vector2.Distance(new Vector2(x,y),new Vector2(_x,_y))<rayon) {
					texture.SetPixel (x, y, colorInk);
				}
			}
		}

		texture.Apply ();
	}
}

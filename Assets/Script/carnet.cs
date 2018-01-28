using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.Audio;

public class carnet : MonoBehaviour {
	private AudioSource audioSrc;
	private Texture2D texture;
	public float _width;
	public float _height;
	public float _offsetX;
	public float _offsetY;
	private int width;
	private int height;
	private int offsetX;
	private int offsetY;
	public bool debug;
	public int rayon;
	public bool pourDessiner;
	public saveAndExit exit;
	private bool isDown;
	public Color colorInk;
	public Texture2D angle;
	private string directoryPath;
	private int nbCarnets;
	private int currentPage = 0;
	private Vector2 oldPos;
	private Vector2 rapportSize;
	public Camera cam;
	public AudioClip[] pageSounds;
	public AudioClip crayonSound;

	// Use this for initialization
	void Start () {
		directoryPath = Application.persistentDataPath + "/carnet/";
		audioSrc = GetComponent<AudioSource> ();

		isDown=false;
		//colorInk = Color.green;
		setScale();

		texture = new Texture2D(width, height);



		Color colorR = Color.clear;
		if (debug) {
			colorR = Color.red;
		}
		for (int y = 0; y < texture.height; y++) {
			for (int x = 0; x < texture.width; x++) {
				
				texture.SetPixel (x, y, colorR);
			}
		}
		texture.Apply ();



		if(!Directory.Exists(directoryPath))
		{    
			//if it doesn't, create it
			Directory.CreateDirectory(directoryPath);

		}

		nbCarnets = 0;

		while(File.Exists(directoryPath+"carnet_"+nbCarnets+".png")) {
			++nbCarnets;
			//Debug.Log ("une page");
		}
		Debug.LogFormat ("nb pages {0}", nbCarnets);

		if (pourDessiner) {
			saveAndExit sAe = Instantiate (exit);
			sAe.car = this;
			audioSrc.clip = crayonSound;
		} else {
			if (File.Exists (directoryPath + "carnet_0.png")) {
				affImage (0);
			}
		}
	}

	void setScale() {
		//Camera cam = GetComponent<Camera>();

		Vector3 sizes = cam.WorldToScreenPoint(new Vector3(_width,_height));
		width = (int)sizes.x;
		height = (int)sizes.y;
		Vector3 offsets = cam.WorldToScreenPoint(new Vector3(_offsetX,_offsetY));
		offsetX = (int)offsets.x;
		offsetY = (int)offsets.y;
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI() {
		if (debug) {
			setScale ();
		}
		Graphics.DrawTexture(new Rect(offsetX, offsetY, width, height), texture);
		if (!pourDessiner && nbCarnets > 1) {
			Graphics.DrawTexture(new Rect(offsetX, offsetY, width, height), angle);
		}
	}

	void OnMouseDown() {
		if (pourDessiner) {
			isDown = true;
			oldPos = (Vector2) Input.mousePosition;

		} else {

		}
	}

	void OnMouseUp() {
		if (pourDessiner) {
			isDown = false;
			audioSrc.Stop ();
		} else {
			currentPage++;
			if (currentPage == nbCarnets)
				currentPage = 0;
			affImage (currentPage);
			audioSrc.clip = pageSounds[(int)Random.Range (0, pageSounds.Length)];
			audioSrc.Play ();
		}
		setScale ();
	}

	void OnMouseExit() {
		if (pourDessiner) {
			isDown = false;
			audioSrc.Stop ();
			//saveToPng();
		}
	}

	void OnMouseOver() {
		if (pourDessiner) {
			Vector2 mpos = (Vector2) Input.mousePosition;
			if (isDown) {
				drawPoint ((int)mpos.x - offsetX, (int)mpos.y - offsetY);
				if (Vector2.Distance (mpos, oldPos) > rayon-1) {
					oldPos = Vector2.Lerp (mpos, oldPos, 0.5f);
					drawPoint ((int)oldPos.x - offsetX, (int)oldPos.y - offsetY);
				}
				texture.Apply ();
				if (!audioSrc.isPlaying) {
					audioSrc.Play ();
				}
				if (oldPos == mpos) {
					audioSrc.Stop ();
				}
				oldPos = mpos;
			}
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
	}

	public void saveToPng() {
		Debug.Log ("saveToPng");
		byte[] bytes = texture.EncodeToPNG();
		File.WriteAllBytes(directoryPath+"carnet_"+nbCarnets+".png", bytes);
		//IEnumerator coroutine = sendPng(texture.EncodeToPNG());
		//StartCoroutine(coroutine);
	}

	void affImage(int num) {
		//directoryPath + "carnet_0.png")

		texture.LoadImage(File.ReadAllBytes(directoryPath + "carnet_"+num+".png"));
	}

	/*IEnumerator sendPng(byte[] file) {
		Debug.Log ("send png");
		List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
		formData.Add( new MultipartFormDataSection("machineId=12&field2=bar") );
		formData.Add( new MultipartFormFileSection("carnet", file, "carnet1.png","image/png") );

		UnityWebRequest www = UnityWebRequest.Post("http://aa-cyclopaedia.fr/machine/upload.php", formData);
		yield return www.SendWebRequest();

		if(www.isNetworkError || www.isHttpError) {
			Debug.Log(www.error);
		}
		else {
			Debug.Log("Form upload complete!");
		}
	}*/
}

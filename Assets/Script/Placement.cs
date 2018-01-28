using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour {
	private int gridWidth;
	private int gridHeight;
	public int nbLignes = 3;
	private int nbCol;
	private int currentCol = 0;
	private int nbModules = 2;
	private int[] nbCases;
	private int[] offsets;
	private float[] scales;
	public SquenceValidator _btnSeq;
	public SelecterGrpValidator _selBtn;
	public Camera cam;


	// Use this for initialization
	void Start () {
		gridHeight = (int)Screen.height / nbLignes;

		nbCol = (int)Screen.width / gridHeight;
		gridWidth = (int)Screen.width / nbCol;


		Debug.LogFormat ("gridWidth : {0} ; gridHeight : {1}", gridWidth,gridHeight);

		nbCases = new int[nbModules];
		offsets = new int[nbModules];
		scales = new float[nbModules];
		/*nbCases[0] = */getNbCases (_btnSeq.GetComponent<Collider2D>(),0);
		/*nbCases[1] = */getNbCases (_selBtn.GetComponent<Collider2D>(),1);

		for (int i=0; i < nbLignes; i++) {
			addComponentsLine (i);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	void getNbCases(Collider2D coll, int id) {
		Debug.LogFormat ("rend.bounds.size : {0}", coll.bounds.size);
		Vector3 taille = coll.bounds.size;
		Vector3 taillePx = cam.WorldToScreenPoint (taille);
		float rapport = taille.x / taille.y;
		int nbCasesTot = 1+(int)(gridHeight * rapport) / gridWidth;
		nbCases [id] = nbCasesTot;
		scales[id] = gridHeight/taillePx.y;
		taillePx = Vector3.Scale(taillePx,new Vector3(scales[id],scales[id],scales[id]));
		offsets[id] = (int) ((gridWidth*nbCasesTot)-taillePx.x)/2;
		Debug.LogFormat ("nbCases : {0} ; scale : {1} ; offset : {2}", nbCasesTot,scales[id],offsets[id]);
		//return nbCases+1;
	}

	void addComponentsLine(int ligne) {
		currentCol = 0;
		while (currentCol < nbCol) {
			int rd = (int)Random.Range (0, 2);
			if (nbCases [rd] + currentCol <= nbCol) {
				switch (rd) {
					case 0:
						SquenceValidator btnSeq = Instantiate (_btnSeq);
					setXYScale (btnSeq.GetComponent<Transform> (), currentCol,ligne,rd);
						break;
					case 1:
						SelecterGrpValidator selBtn = Instantiate (_selBtn);
					setXYScale (selBtn.GetComponent<Transform> (), currentCol,ligne,rd);
						break;
				}
				currentCol += nbCases [rd];
			}
		}
	}

	void setXYScale(Transform obj, int x, int y,int id) {
		//Debug.LogFormat ("papa.bounds.size : {0}", papa.bounds.size);
		//getNbCases (obj);
		obj.position = cam.ScreenToWorldPoint(new Vector3(x*gridWidth+gridWidth/2+offsets[id],y*gridHeight+gridHeight/2,1));
		obj.localScale = new Vector3 (scales[id],scales[id],scales[id]);
	}
}

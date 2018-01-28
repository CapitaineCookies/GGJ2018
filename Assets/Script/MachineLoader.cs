using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineLoader : MonoBehaviour {

	public Machine machine;
	public SquenceValidator sequenceGroup;
	public SelecterButtonGroup selecterButtonGroup;
	public SelecterRouletteGroup selecterRouletteGroup;
	public Validable[] validables;
	//public int validableNumberToCreate = 3;

	private int gridWidth;
	private int gridHeight;
	public int nbLignes = 3;
	private int nbCol;
	//private int nbModules = 2;
	private int[] nbCases;
	private int[] offsets;
	private float[] scales;
	public Camera cam;


	// Use this for initialization
	void Start () {
		gridHeight = (int)Screen.height / nbLignes;
		nbCol = (int)Screen.width / gridHeight;
		gridWidth = (int)Screen.width / nbCol;
		nbCases = new int[validables.Length];
		offsets = new int[validables.Length];
		scales = new float[validables.Length];

		for (int i = 0; i < validables.Length; i++) {
			getNbCases (validables[i].GetComponent<BoxCollider2D>(),i);
		}

		SaveLoadManager.LoadMachine (this);
		Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnLevelWasLoaded() {
		Debug.Log ("MachineLoader OnLevelWasLoaded");
	}

	public void InstanciateNewMachine() {

		/*for (int i = 0; i < validableNumberToCreate; i++) {
			int validableIndex = Random.Range (0, validables.Length);
			Validable variable = Instantiate (validables[validableIndex], machine.GetComponent<Transform>());
			Transform transform = variable.GetComponent<Transform> ();
			transform.position = new Vector3(Random.Range (-50f, 50f), Random.Range (-50f, 50f), transform.position.z);
		}*/

		for (int i=0; i < nbLignes; i++) {
			addComponentsLine (i);
		}
	}

	void getNbCases(BoxCollider2D coll, int id) {
		Vector3 taille = new Vector3(coll.size.x,coll.size.y,1);
		Vector3 taillePx = cam.WorldToScreenPoint (taille);
		float rapport = taille.x / taille.y;
		int nbCasesTot = 1+(int)(gridHeight * rapport) / gridWidth;
		nbCases [id] = nbCasesTot;
		scales[id] = gridHeight/taillePx.y;
		taillePx = Vector3.Scale(taillePx,new Vector3(scales[id],scales[id],scales[id]));
		//offsets[id] = (int) ((gridWidth*nbCasesTot)-taillePx.x)/2;
		Debug.LogFormat ("nbCases : {0} ; scale : {1} ; offset : {2}", nbCasesTot,scales[id],offsets[id]);
		//return nbCases+1;
		//Destroy(coll);
		coll.enabled = false;
	}

	void addComponentsLine(int ligne) {
		int currentCol = 0;
		while (currentCol < nbCol) {
			int rd = (int)Random.Range (0, validables.Length);
			if (nbCases [rd] + currentCol <= nbCol) {
				Validable variable = Instantiate (validables[rd], machine.GetComponent<Transform>());
				setXYScale (variable.GetComponent<Transform> (), currentCol,ligne,rd);
				/*switch (rd) {
				case 0:
					SquenceValidator btnSeq = Instantiate (_btnSeq);
					setXYScale (btnSeq.GetComponent<Transform> (), currentCol,ligne,rd);
					break;
				case 1:
					SelecterGrpValidator selBtn = Instantiate (_selBtn);
					setXYScale (selBtn.GetComponent<Transform> (), currentCol,ligne,rd);
					break;
				}*/
				currentCol += nbCases [rd];
			}
		}
	}

	void setXYScale(Transform obj, int x, int y,int id) {
		Debug.LogFormat ("x : {0} ; y : {1}", x,y);
		//BoxCollider2D coll = obj.GetComponent<BoxCollider2D> ();
		//getNbCases (obj);
		obj.position = cam.ScreenToWorldPoint(new Vector3(x*gridWidth+gridWidth/2,y*gridHeight+gridHeight/2,1));
		obj.localScale = new Vector3 (scales[id],scales[id],1);
		//float offsetX = (cam.ScreenToWorldPoint(new Vector3(gridWidth * nbCases [id],1,1)).x - coll.bounds.size.x) / 2;
		//obj.position += new Vector3(coll.offset.x,coll.offset.y,1);
	}

	private void DeserializeGroup(Validable validable, GroupData data) {
		Transform transform = validable.GetComponent<Transform> ();
		transform.localPosition = data.position.Deserialize ();
		transform.localRotation = data.rotation.Deserialize ();
		transform.localScale = data.scale.Deserialize ();
	}

	private void DeserializeSelecterGroup(SelecterGrpValidator selecter, SelecterGroupData data) {
		selecter.targetPositions = data.targetPositions;
	}

	public void DeserializeSequenceGroup(Transform parent, SequenceGroupData data) {
		SquenceValidator sequenceGroupCpy = Instantiate (sequenceGroup, parent);
		DeserializeGroup (sequenceGroupCpy, data);
		sequenceGroupCpy.sequenceToDo = data.sequenceToDo;
	}

	public void DeserializeSelecterButtonGroup(Transform parent, SelecterGroupData data) {
		SelecterButtonGroup selecterGroupCpy = Instantiate (selecterButtonGroup, parent);
		DeserializeGroup (selecterGroupCpy, data);
		DeserializeSelecterGroup (selecterGroupCpy, data);
	}

	public void DeserializeSelecterRouletteGroup(Transform parent, SelecterRouletteGroupData data) {
		SelecterRouletteGroup selecterGroupCpy = Instantiate (selecterRouletteGroup, parent);
		DeserializeGroup (selecterGroupCpy, data);
		DeserializeSelecterGroup (selecterGroupCpy, data);
	}

	
}

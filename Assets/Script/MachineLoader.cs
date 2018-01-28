using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineLoader : MonoBehaviour {

	public Machine machine;
	public SquenceValidator sequenceGroup;
	public SelecterGrpValidator selecterGroup;
	public Validable[] validables;
	public int validableNumberToCreate = 3;

	void Awake() {
		Debug.Log ("MachineLoader Awake");
	}

	// Use this for initialization
	void Start () {
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

		for (int i = 0; i < validableNumberToCreate; i++) {
			int validableIndex = Random.Range (0, validables.Length);
			Validable variable = Instantiate (validables[validableIndex], machine.GetComponent<Transform>());
			Transform transform = variable.GetComponent<Transform> ();
			transform.position = new Vector3(Random.Range (-50f, 50f), Random.Range (-50f, 50f), transform.position.z);
		}
	}

	public void DeserializeSequenceGroup(Transform parent, SequenceGroupData data) {
		SquenceValidator sequenceGroupCpy = Instantiate (sequenceGroup, parent);
		Transform transform = sequenceGroupCpy.GetComponent<Transform> ();
		transform.localPosition = data.position.Deserialize ();
		transform.localRotation = data.rotation.Deserialize ();
		transform.localScale = data.scale.Deserialize ();
		sequenceGroupCpy.sequenceToDo = data.sequenceToDo;
	}

	public void DeserializeSelecterGroup(Transform parent, SelecterGroupData data) {
		SelecterGrpValidator selecterGroupCpy = Instantiate (selecterGroup, parent);
		Transform transform = selecterGroupCpy.GetComponent<Transform> ();
		transform.localPosition = data.position.Deserialize ();
		transform.localRotation = data.rotation.Deserialize ();
		transform.localScale = data.scale.Deserialize ();
		selecterGroupCpy.targetPositions = data.targetPositions;
	}
}

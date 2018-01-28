using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public static class SaveLoadManager {
	

	public static void Save(Machine machine) {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream stream = File.Open (getFilePath(), FileMode.OpenOrCreate);

		MachineData machinData = new MachineData (machine);
		bf.Serialize (stream, machinData);
		stream.Close ();
	}

	public static void LoadMachine(MachineLoader machineLoader) {
		if (File.Exists (getFilePath())) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (getFilePath(), FileMode.Open);
			MachineData data = bf.Deserialize (file) as MachineData;
			InstanciateMachine (machineLoader, data);
			file.Close ();
		} else {
			machineLoader.InstanciateNewMachine ();
			SaveLoadManager.Save (machineLoader.machine);
		}
	}

	private static string getFilePath() {
		return Application.persistentDataPath + "/machine.sav";
	}

	private static void InstanciateMachine(MachineLoader machineLoader, MachineData machineData) {
		Transform parent = machineLoader.machine.GetComponent<Transform> ();
		foreach (ValidableData validableData in machineData.validablesData) {
			if (validableData is SequenceGroupData) {
				machineLoader.DeserializeSequenceGroup (parent, (SequenceGroupData)validableData);
			} else if (validableData is SelecterGroupData) {
				machineLoader.DeserializeSelecterGroup (parent, (SelecterGroupData)validableData);

			}
		}
	}


}

[Serializable]
public class MachineData {
	
	public List<ValidableData> validablesData;

	public MachineData() {

	}

	public MachineData(Machine machine) {
		SquenceValidator[] seqValidables = machine.GetComponentsInChildren <SquenceValidator>();
		SelecterGrpValidator[] selecterValidables = machine.GetComponentsInChildren <SelecterGrpValidator>();

		validablesData = new List<ValidableData>();
		foreach (SquenceValidator validable in seqValidables) {
			validablesData.Add (new SequenceGroupData(validable));
		}
		foreach (SelecterGrpValidator validable in selecterValidables) {
			validablesData.Add (new SelecterGroupData(validable));
		}
	}

	public void Deserialize(Machine machine) {
		
	}
}

[Serializable]
public abstract class ValidableData {

}

[Serializable]
public class SequenceGroupData : ValidableData {
	public Vector3Data position;
	public QuaternionData rotation;
	public Vector3Data scale;
	public int[] sequenceToDo;

	public SequenceGroupData(SquenceValidator sequence) {
		Transform transform = sequence.GetComponent<Transform> ();
		
		position = new Vector3Data(transform.localPosition);
		rotation = new QuaternionData(transform.localRotation);
		scale = new Vector3Data(transform.localScale);
		sequenceToDo = sequence.sequenceToDo;
	}

}

[Serializable]
public class SelecterGroupData : ValidableData {
	public Vector3Data position;
	public QuaternionData rotation;
	public Vector3Data scale;
	public int[] targetPositions;

	public SelecterGroupData(SelecterGrpValidator sequence) {
		Transform transform = sequence.GetComponent<Transform> ();

		position = new Vector3Data(transform.localPosition);
		rotation = new QuaternionData(transform.localRotation);
		scale = new Vector3Data(transform.localScale);
		targetPositions = sequence.targetPositions;
	}
}

[Serializable]
public class Vector3Data {
	float x;
	float y;
	float z;

	public Vector3Data(Vector3 vector) {
		x = vector.x;
		y = vector.y;
		z = vector.z;
	}

	public Vector3 Deserialize() {
		return new Vector3 (x, y, z);
	}
}


[Serializable]
public class QuaternionData {

	float x;
	float y;
	float z;
	float w;

	public QuaternionData(Quaternion quaternion) {
		x = quaternion.x;
		y = quaternion.y;
		z = quaternion.z;
		w = quaternion.w;
	}

	public Quaternion Deserialize() {
		return new Quaternion (x, y, z, w);
	}
}
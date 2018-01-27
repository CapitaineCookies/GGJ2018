using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public static class SaveLoadManager {

	public static void Save(Player player) {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream stream = File.Open (getFilePath(), FileMode.OpenOrCreate);

		//PlayerData data = new PlayerData (player);

		//bf.Serialize (stream, data);
		stream.Close ();
	}

	public static SequenceValidatorData LoadPlayer() {
		if (File.Exists (getFilePath())) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (getFilePath(), FileMode.Open);
			//PlayerData data = bf.Deserialize (file) as PlayerData;
			file.Close ();
			return null;
		} else {
			return new SequenceValidatorData();
		}
	}

	private static string getFilePath() {
		return Application.persistentDataPath + "/player.sav";
	}
}

[Serializable]
public class MachinData {
	public float health;
	public float experience;

	public MachinData() {

	}

	public MachinData(Player player) {
		health = player.health;
		experience = player.experience;
	}
}

[Serializable]
public abstract class ValidableData {

}

[Serializable]
public class SequenceValidatorData : ValidableData {
	float x;
	float y;

}
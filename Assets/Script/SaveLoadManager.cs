using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public static class SaveLoadManager {

	public static void Save(Player player) {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (getFilePath(), FileMode.OpenOrCreate);

		PlayerData data = new PlayerData (player);

		bf.Serialize (file, data);
		file.Close ();
	}

	public static PlayerData LoadPlayer() {
		if (File.Exists (getFilePath())) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (getFilePath(), FileMode.Open);

			PlayerData data = bf.Deserialize (file) as PlayerData;
			file.Close ();
			return data;
		} else {
			return new PlayerData();
		}
	}

	private static string getFilePath() {
		return Application.persistentDataPath + "/player.sav";
	}
}

[Serializable]
public class PlayerData {
	public float health;
	public float experience;

	public PlayerData() {

	}

	public PlayerData(Player player) {
		health = player.health;
		experience = player.experience;
	}
}
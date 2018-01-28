using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Victory : MonoBehaviour {
	private string directoryPath;

	// Use this for initialization
	void Start () {
		directoryPath = Application.persistentDataPath + "/carnet/";
		Directory.Move(directoryPath+"carnet_0.png",directoryPath+"start.png");

		File.Copy(Application.dataPath+"/img/message_fin.png",directoryPath+"carnet_0.png");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Victory : MonoBehaviour {
	private string directoryPath;

	// Use this for initialization
	void Start () {
		directoryPath = Application.persistentDataPath + "/carnet/";
		FileUtil.MoveFileOrDirectory(directoryPath+"carnet_0.png",directoryPath+"start.png");
		FileUtil.CopyFileOrDirectory(Application.dataPath+"/img/message_fin.png",directoryPath+"carnet_0.png");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

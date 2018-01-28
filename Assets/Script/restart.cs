using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class restart : MonoBehaviour
{
	private string directoryPath;
	// Use this for initialization
	void Start ()
	{
		directoryPath = Application.persistentDataPath + "/";
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnMouseUp ()
	{
		int numMachine = 0;
		while (Directory.Exists (directoryPath + "Machine" + numMachine)) {
			numMachine++;
		}

		Directory.Move (directoryPath + "carnet/", directoryPath + "Machine" + numMachine + "/");
		Directory.Move (directoryPath + "machine.sav", directoryPath + "Machine" + numMachine + "/machine.sav");
		SceneManager.LoadScene ("carnet");
	}
}

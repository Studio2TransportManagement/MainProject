using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class LoadNamesFromTextFile : MonoBehaviour {

	public string[] a_sNames;

	// Use this for initialization
	void Start () {
		try {
			a_sNames = File.ReadAllLines("Assets/Scripts/SoldierNames.txt");
		}
		catch(IOException e) {
			Debug.Log("<color=red>Read error! </color> " + e.Message);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

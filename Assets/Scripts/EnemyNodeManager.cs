using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

public class EnemyNodeManager : MonoBehaviour {

	public static EnemyNodeManager instance = null;
	public static List<Vector3> sl_vDestinations;
	private static string[] sa_sLoadIn;

	// Use this for initialization
	void Start() {
	
	}

	void Awake() {
		if (instance != null) {
			Debug.LogError("Can't have more than 1 instance of EnemyNodeManager active!");
			Debug.Break();
		}
		else {
			instance = this;
			sl_vDestinations = new List<Vector3>();

			try {
				sa_sLoadIn = File.ReadAllLines("Assets/Scripts/EnemyGoalNodes.txt");
			}
			catch(IOException e) {
				Debug.Log("<color=red>Read error! </color> " + e.Message);
			}

			//Split string
			string[] a_sSplit;
			for (int i = 0; i < sa_sLoadIn.Length; i++) {
				a_sSplit = sa_sLoadIn[i].Split(char.Parse(","));
				sl_vDestinations.Add(new Vector3(float.Parse(a_sSplit[0]),
				                                 float.Parse(a_sSplit[1]),
				                                 float.Parse(a_sSplit[2])));
			}
		}

		//Test
		foreach (Vector3 vec in sl_vDestinations) {
			Debug.Log (vec.ToString());
		}
	}
	
	// Update is called once per frame
	void Update() {
	
	}
}

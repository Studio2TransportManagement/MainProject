using UnityEngine;
using System.Collections.Generic;

public class NameSaver : MonoBehaviour {
	
	public static NameSaver Instance;
	
	public List<string> l_sDeadUnitNames;

	void Start() {
		l_sDeadUnitNames = new List<string>();
	}

	void Update() {

	}
	
	void Awake() {
		if (Instance) {
			DestroyImmediate(gameObject);
		}
		else {
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}
	}
}

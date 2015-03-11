using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DisplayTextList : MonoBehaviour {
	public Canvas cnvCanvas;
	public Text txtText;
	private string sDisplay = "";
	private List<string> l_sStorage;
	
	void Start() {
		l_sStorage = new List<string>();
		//l_sStorage.Add("this ");
	}
	
	
	void Update() {
		ReDrawText();
	}

	public void ClearText() {
		l_sStorage.Clear();
		txtText.text = "";
	}

	public void AddText(string str) {
		l_sStorage.Add(str);
	}

	public void RemoveText(string str) {
		if (l_sStorage.Contains(str)) {
			l_sStorage.RemoveAt(l_sStorage.IndexOf(str));
		}
	}
	
	public void ReDrawText() {
		sDisplay = "";
		foreach (string msg in l_sStorage) {
			sDisplay = sDisplay + msg + "\n";
		}
		txtText.text = sDisplay;
	}
}
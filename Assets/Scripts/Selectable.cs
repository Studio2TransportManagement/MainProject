using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour {

	public bool bUnselectable;
	private SelectionManager selectionManager;

	// Use this for initialization
	void Start () {
		selectionManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SelectionManager>();
	}

	public bool IsSelected() {
		if (selectionManager.GetSelection() == this) {
			Debug.Log("<color=purple>" + this.gameObject.name + " is selected</color>");
			return true;
		}

		Debug.Log("<color=purple>" + this.gameObject.name + " :: this</color>");
		Debug.Log("<color=purple>" + selectionManager.GetSelection().name + " :: selman selected</color>");

		return false;
	}
	
	// Update is called once per frame
	void Update () {
		//
	}
}

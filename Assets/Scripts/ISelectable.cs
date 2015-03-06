using UnityEngine;
using System.Collections;

public interface ISelectable {

	bool bUnselectable {
		get;
		set;
	}

	SelectionManager selectionManager {
		get;
		set;
	}
	
	bool IsSelected();
}

// Use this for initialization
//	void Start () {
//		selectionManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SelectionManager>();
//	}

//		if (selectionManager.GetSelection() == this) {
//			Debug.Log("<color=purple>" + this.gameObject.name + " is selected</color>");
//			return true;
//		}
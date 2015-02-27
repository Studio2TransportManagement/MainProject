using UnityEngine;
using System.Collections;

public class SelectionManager : MonoBehaviour {

	private GameObject goCurrentSelection;
	private bool bGotSelection = false;

	//Init
	void Start () {
		goCurrentSelection = null;
	}
	
	//Update
	void Update () {
		//If there is no game object, make sure we don't do anything with the selection
		if (goCurrentSelection == null) {
			bGotSelection = false;
		}
	}

	public GameObject GetSelection() {
		return goCurrentSelection;
	}

	//Only "Selectable" objects can be selected
	public bool SetSelection(GameObject go) {
		if (go.GetComponent<Selectable>()) {
			if (go.GetComponent<Selectable>().bUnselectable == false) {
				goCurrentSelection = go;

				return true;
			}
		}

		return false;
	}

	public void ClearSelection() {
		goCurrentSelection = null;
	}

}

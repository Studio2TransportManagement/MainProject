using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class SelectionManager : MonoBehaviour {

	private GameObject goCurrentSelection;
	private bool bGotSelection = false;

	private RaycastHit hit;
	private Ray rRay;
	
	//Init
	void Start () {
		goCurrentSelection = null;

		hit = new RaycastHit();
	}
	
	//Update
	void Update () {
		//Check for selectable objects under the mouse, but ignore GUIs
		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
			rRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(rRay, out hit, Mathf.Infinity)) {
				if (hit.transform.gameObject.GetComponent<Selectable>() != null) {
					Debug.Log("<color=green>Hit selectable!</color>");
					//Don't select the same object twice
					if (hit.transform.gameObject != goCurrentSelection) {
						goCurrentSelection = hit.transform.gameObject;

						if (goCurrentSelection.tag == "building") {
							Debug.Log("Hit building");
						}

						if (goCurrentSelection.tag == "player-unit") {
							Debug.Log("Hit player unit");
						}

						//If the object had a GUI menu, activate it now
						if (goCurrentSelection.GetComponent<GUI_Base>()) {
							goCurrentSelection.GetComponent<GUI_Base>().OnSelected();
						}
					}
				}
				else {
					//If selection had a GUI component, run deselected function
					if (goCurrentSelection != null) {
						if (goCurrentSelection.GetComponent<GUI_Base>()) {
							goCurrentSelection.GetComponent<GUI_Base>().OnDeselected();
						}
					}
					//Debug.Log(hit.transform.gameObject.name);
					Debug.Log("<color=red>Lost selection!</color>");
					goCurrentSelection = null;
				}
			}
		}

		//If we right click, issue an order
		if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject() && goCurrentSelection != null) {
			rRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(rRay, out hit, Mathf.Infinity)) {
				//If it was a unit, tell it to move
				if (goCurrentSelection.tag == "player-unit") {
					goCurrentSelection.GetComponent<SlideToLocation>().vTarget = hit.point;
				}
			}
		}

		//If there is no game object, make sure we don't do anything with the selection
		if (goCurrentSelection == null) {
			bGotSelection = false;
		}
		else {
			bGotSelection = true;
		}
	}

	public GameObject GetSelection() {
		return goCurrentSelection;
	}

	public bool HasSelection() {
		return bGotSelection;
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

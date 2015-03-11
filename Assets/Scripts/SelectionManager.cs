using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SelectionManager : MonoBehaviour {

	private List<GameObject> l_goCurrentSelection;
	private bool bGotSelection = false;

	private RaycastHit hit;
	private Ray rRay;

	public DisplayTextList displayNames;
	
	//Init
	void Start() {
		l_goCurrentSelection = new List<GameObject>();

		hit = new RaycastHit();
	}
	
	//Update
	void Update() {
		//Check for selectable objects under the mouse, but ignore GUIs
		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
			rRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(rRay, out hit, Mathf.Infinity)) {
				//if (hit.transform.gameObject.GetComponent("ISelectable") as ISelectable != null) {
				if (hit.transform.gameObject.GetComponent<GameUnit>() != null) {
					//Debug.Log("<color=green>Hit selectable!</color>");
					//Don't select the same object twice
					if (!l_goCurrentSelection.Contains(hit.transform.gameObject)) {
						l_goCurrentSelection.Add(hit.transform.gameObject);

						if (l_goCurrentSelection[l_goCurrentSelection.Count - 1].tag == "building") {
							Debug.Log("Clicked on the <color=green>" + l_goCurrentSelection[l_goCurrentSelection.Count - 1].GetComponent<GameUnit>().sUnitName + "</color>!");
						}

						if (l_goCurrentSelection[l_goCurrentSelection.Count - 1].tag == "player-unit") {
							displayNames.AddText(l_goCurrentSelection[l_goCurrentSelection.Count - 1].GetComponent<GameUnit>().sUnitName);
							Debug.Log("Clicked on <color=blue>" + l_goCurrentSelection[l_goCurrentSelection.Count - 1].GetComponent<GameUnit>().sUnitName + "</color>!");
						}

						//If the object had a GUI menu, activate it now
						if (l_goCurrentSelection[l_goCurrentSelection.Count - 1].GetComponent<GUI_Base>()) {
							l_goCurrentSelection[l_goCurrentSelection.Count - 1].GetComponent<GUI_Base>().OnSelected();
						}
					}
					else {
						//Unselect if clicked again
						displayNames.RemoveText(hit.transform.gameObject.GetComponent<GameUnit>().sUnitName);
						l_goCurrentSelection.Remove(hit.transform.gameObject);
					}
				}
				else {
					//If selection had a GUI component, run deselected function
					if (l_goCurrentSelection.Count > 0) {
						foreach (GameObject gobj in l_goCurrentSelection) {
							if (gobj.GetComponent<GUI_Base>()) {
								gobj.GetComponent<GUI_Base>().OnDeselected();
							}
						}
					}
					//Debug.Log(hit.transform.gameObject.name);
					ClearSelection();
				}
			}
		}

		//If we right click, issue an order
		if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject() && l_goCurrentSelection.Count > 0) {
			rRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(rRay, out hit, Mathf.Infinity)) {
				foreach (GameObject gobj in l_goCurrentSelection) {
					//If it was a unit, tell it to move
					if (gobj.tag == "player-unit") {
						gobj.GetComponent<SlideToLocation>().vTarget = hit.point;
					}
				}
			}
		}

		//If there is no game object, make sure we don't do anything with the selection
		if (l_goCurrentSelection.Count > 0) {
			bGotSelection = false;
		}
		else {
			bGotSelection = true;
		}
	}

	public List<GameObject> GetSelection() {
		return l_goCurrentSelection;
	}

	public bool IsAlreadySelected(GameObject go) {
		return l_goCurrentSelection.Contains(go);
	}

	public bool HasSelection() {
		return bGotSelection;
	}

	//Only "Selectable" objects can be selected
	public bool SetSelection(GameUnit go) {
		//if (go.GetComponent("ISelectable") as GameUnit != null) {
		if (go.GetComponent<GameUnit>() != null) {
			if (go.bUnselectable == false) {
				l_goCurrentSelection.Add(go.gameObject);

				return true;
			}
		}

		return false;
	}

	public void ClearSelection() {
		Debug.Log("<color=magenta>Lost selection!</color>");
		l_goCurrentSelection.Clear();
		displayNames.ClearText();
	}

}

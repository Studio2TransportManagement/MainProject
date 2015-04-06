﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SelectionManager : MonoBehaviour {

	public List<GameObject> l_goCurrentSelection;
	public Camera cMainCamera;
	private bool bGotSelection = false;

	private RaycastHit hit;
	private Ray rRay;

	public DisplayTextList displayNames;

	public GameObject goCurrentObject;
	public GameUnit guCurrentUnit;
	public GameStructure gsCurrentBuilding;
	public GUI_Base guiCurrentGUIBase;
	
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

				if (IsObjectSelectable(hit.transform.gameObject)) {
					goCurrentObject = hit.transform.gameObject;
					//Don't select the same object twice
					if (!l_goCurrentSelection.Contains(goCurrentObject)) {
						
						if (hit.transform.gameObject.GetComponent<GameUnit>()) {
							l_goCurrentSelection.Add(goCurrentObject);
							if(!l_goCurrentSelection[0].GetComponent<GameUnit>())
							{
								ClearSelection();
							}
							guCurrentUnit = hit.transform.gameObject.GetComponent<GameUnit>();
						}
						if (goCurrentObject.GetComponent<GameStructure>()) {
							ClearSelection();
							l_goCurrentSelection.Add(goCurrentObject);
							gsCurrentBuilding = hit.transform.gameObject.GetComponent<GameStructure>();
							guiCurrentGUIBase = hit.transform.gameObject.GetComponent<GUI_Base>();
							guiCurrentGUIBase.OnSelected();
						}

						if (goCurrentObject.tag == "player-unit") {
							displayNames.AddText(guCurrentUnit.sUnitName);
							Debug.Log("Clicked on <color=blue>" + guCurrentUnit.sUnitName + "</color>!");
						}

						if (goCurrentObject.tag == "building") {
							displayNames.AddText(gsCurrentBuilding.sBaseName);
							Debug.Log("Clicked on <color=blue>" + gsCurrentBuilding.sBaseName + "</color>!");
						}
						
	//					if (goCurrentObject.tag == "train") {
	//						displayNames.AddText(guCurrentUnit.sUnitName);
	//						Debug.Log("Clicked on <color=blue>" + guCurrentUnit.sUnitName + "</color>, choo choo!");
	//					}
					}
					else {
						//Unselect if clicked again
						if (guCurrentUnit != null) {
							displayNames.RemoveText(guCurrentUnit.sUnitName);
						}
						//If selection had a GUI component, run deselected function
						if (gsCurrentBuilding != null) {
							displayNames.RemoveText (gsCurrentBuilding.sBaseName);
						}
						l_goCurrentSelection.Remove(goCurrentObject);
			         }
				}
				else {
					//Debug.Log(hit.transform.gameObject.name);
					Debug.Log("Nothing interesting here..");
					if (gsCurrentBuilding != null) {
						guiCurrentGUIBase.OnDeselected();
					}
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
						if (hit.transform.gameObject.tag == "train") {
							gobj.GetComponent<NavMeshAgent>().SetDestination(hit.point);
							hit.transform.gameObject.GetComponent<Train>().AddExpected(gobj.GetComponent<GameUnit>());
						}
						else {
							gobj.GetComponent<NavMeshAgent>().SetDestination(hit.point);
						}
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
		//grab all the selected units and setactive false their goHealthInstans.
		l_goCurrentSelection.Clear();
		displayNames.ClearText();
//		goCurrentObject = null;
		guCurrentUnit = null;
		guiCurrentGUIBase = null;
		gsCurrentBuilding = null;
	}

	public void RemoveDeadUnitIfSelected(GameObject deadUnit) {
		if (l_goCurrentSelection.Contains (deadUnit)) {
			l_goCurrentSelection.Remove (deadUnit);
			displayNames.RemoveText(deadUnit.GetComponent<GameUnit>().sUnitName);
		}
	}

	private bool IsObjectSelectable(GameObject go) {
		if (go.GetComponent<GameUnit>() != null) {
			return true;
		}
		else if (go.GetComponent<GameStructure>() != null) {
			return true;
		}

		return false;
	}

}

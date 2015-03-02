using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChildMenuController : GUI_Base {
	

	public Button[] a_buttons;

	//Init
	void Start() {
		a_buttons = GetComponentsInChildren<Button>();
	}

	public void OpenMenu() {
		foreach (Button but in a_buttons) {
			but.GetComponent<GUIClickMenu>().bMenuOn = true;
		}
	}

	public void CloseMenu() {
		foreach (Button but in a_buttons) {
			but.GetComponent<GUIClickMenu>().bMenuOn = false;
		}
	}

	//Update 
	void Update() {
		//
	}

	//Inherited
	public override void OnSelected() {
		OpenMenu();
	}

	public override void OnDeselected() {
		CloseMenu();
	}
}

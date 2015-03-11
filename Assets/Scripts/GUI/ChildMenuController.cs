using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChildMenuController : GUI_Base {
	
	public GameObject recruitmentPanel;
	//public Button[] a_buttons;

	//Init
	void Start() {
		//a_buttons = GetComponentsInChildren<Button>();
	}

	public void OpenMenu() {
		LeanTween.move (recruitmentPanel, recruitmentPanel.transform.position + new Vector3 (0f, 520f, 0f), 0.25f).setEase (LeanTweenType.easeInQuad);
//		foreach (Button but in a_buttons) {
//			but.GetComponent<GUIClickMenu>().bMenuOn = true;
//		}
	}

	public void CloseMenu() {
		LeanTween.move (recruitmentPanel, recruitmentPanel.transform.position + new Vector3 (0f, -520f, 0f), 0.25f).setEase (LeanTweenType.easeInQuad);
//		foreach (Button but in a_buttons) {
//			but.GetComponent<GUIClickMenu>().bMenuOn = false;
//		}
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

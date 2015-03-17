using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChildMenuController : GUI_Base {
	
	public GameObject recruitmentPanel;

	//Init
	void Start() {
		//
	}

	public void OpenMenu() {
		LeanTween.move (recruitmentPanel, recruitmentPanel.transform.position + new Vector3 (0f, 1040f, 0f), 0.25f).setEase (LeanTweenType.easeInQuad);
	}

	public void CloseMenu() {
		LeanTween.move (recruitmentPanel, recruitmentPanel.transform.position + new Vector3 (0f, -1040f, 0f), 0.25f).setEase (LeanTweenType.easeInQuad);
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

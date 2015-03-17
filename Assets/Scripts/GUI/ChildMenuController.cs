using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChildMenuController : GUI_Base {
	
	public GameObject goRecruitmentPanel;
	public bool bIsPanelOpen = false;
	public float fTimer = 0f;

	//Init
	void Start() {
		//
	}

	public void OpenMenu() {
		if(!bIsPanelOpen)
		{
			LeanTween.move (goRecruitmentPanel, goRecruitmentPanel.transform.position + new Vector3 (0f, 1040f, 0f), 0.25f).setEase (LeanTweenType.easeInQuad);
			bIsPanelOpen = true;
		}
	}

	public void CloseMenu() {
		if(bIsPanelOpen)
		{
			LeanTween.move (goRecruitmentPanel, goRecruitmentPanel.transform.position + new Vector3 (0f, -1040f, 0f), 0.25f).setEase (LeanTweenType.easeInQuad);
			bIsPanelOpen = false;
		}
	}

	//Update 
	void Update() {
		fTimer -= Time.deltaTime;
	}

	//Inherited
	public override void OnSelected() {
		if(fTimer <= 0)
		{
			OpenMenu();
			fTimer = 0.30f;
		}
	}

	public override void OnDeselected() {
		if(fTimer <= 0)
		{
			CloseMenu();
			fTimer = 0.30f;
		}
	}
}

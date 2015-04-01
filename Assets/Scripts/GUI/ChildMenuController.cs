using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChildMenuController : GUI_Base {

	public GameObject goRecruitmentPanel;
	public GameObject goUpgradePanel;
	static bool bIsRecruitmentPanelOpen = false;
	static bool bIsUpgradePanelOpen = false;
	public float fTimer = 0f;

	//Init
	void Start() {
		//
	}

	public void OpenMenu() {
		if (this.gameObject.name.StartsWith("Base ") && !bIsRecruitmentPanelOpen && !bIsUpgradePanelOpen) {
			LeanTween.move (goUpgradePanel, goUpgradePanel.transform.position + new Vector3 (0f, -1040f, 0f), 0.25f).setEase (LeanTweenType.easeInQuad);
			bIsUpgradePanelOpen = true;
		}
		else if (this.gameObject.name == "Recruitment Building" && !bIsRecruitmentPanelOpen && !bIsUpgradePanelOpen) {
			LeanTween.move (goRecruitmentPanel, goRecruitmentPanel.transform.position + new Vector3 (0f, 1040f, 0f), 0.25f).setEase (LeanTweenType.easeInQuad);
			bIsRecruitmentPanelOpen = true;
		}
		
		if(this.gameObject.name == "Recruitment Building" && bIsUpgradePanelOpen)
		{
			LeanTween.move (goUpgradePanel, goUpgradePanel.transform.position + new Vector3 (0f, 1040f, 0f), 0.25f).setEase (LeanTweenType.easeInQuad);
			bIsUpgradePanelOpen = false;
			LeanTween.move (goRecruitmentPanel, goRecruitmentPanel.transform.position + new Vector3 (0f, 1040f, 0f), 0.25f).setEase (LeanTweenType.easeInQuad);
			bIsRecruitmentPanelOpen = true;
		}

		if(this.gameObject.name.StartsWith("Base ") && bIsRecruitmentPanelOpen)
		{
			LeanTween.move (goRecruitmentPanel, goRecruitmentPanel.transform.position + new Vector3 (0f, -1040f, 0f), 0.25f).setEase (LeanTweenType.easeInQuad);
			bIsRecruitmentPanelOpen = false;
			LeanTween.move (goUpgradePanel, goUpgradePanel.transform.position + new Vector3 (0f, -1040f, 0f), 0.25f).setEase (LeanTweenType.easeInQuad);
			bIsUpgradePanelOpen = true;
		}

	}

	public void CloseMenu() {
		if(bIsRecruitmentPanelOpen)
		{
			LeanTween.move (goRecruitmentPanel, goRecruitmentPanel.transform.position + new Vector3 (0f, -1040f, 0f), 0.25f).setEase (LeanTweenType.easeInQuad);
			bIsRecruitmentPanelOpen = false;
		}

		if(bIsUpgradePanelOpen)
		{
			LeanTween.move (goUpgradePanel, goUpgradePanel.transform.position + new Vector3 (0f, 1040f, 0f), 0.25f).setEase (LeanTweenType.easeInQuad);
			bIsUpgradePanelOpen = false;
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
			fTimer = 0.25f;
		}
	}

	public override void OnDeselected() {
		if(fTimer <= 0)
		{
			CloseMenu();
			fTimer = 0.25f;
		}
	}
}

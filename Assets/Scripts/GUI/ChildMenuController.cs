using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChildMenuController : GUI_Base {

	public GameUnit guRecruitmentBuilding;
	public GameObject goRecruitmentPanel;
	public GameUnit guBase1Building;
	public GameUnit guBase2Building;
	public GameUnit guBase3Building;
	public GameObject goUpgradePanel;
	static bool bIsRecruitmentPanelOpen = false;
	static bool bIsUpgradePanelOpen = false;
	public float fTimer = 0f;

	//Init
	void Start() {
		//
	}

	public void OpenMenu() {
		if(guRecruitmentBuilding.IsSelected() && !bIsRecruitmentPanelOpen && !bIsUpgradePanelOpen)
		{
			LeanTween.move (goRecruitmentPanel, goRecruitmentPanel.transform.position + new Vector3 (0f, 1040f, 0f), 0.25f).setEase (LeanTweenType.easeInQuad);
			bIsRecruitmentPanelOpen = true;
		}

		if((guBase1Building.IsSelected() || 
		    guBase2Building.IsSelected() ||
		    guBase3Building.IsSelected() ) && !bIsRecruitmentPanelOpen && !bIsUpgradePanelOpen)
		{
			LeanTween.move (goUpgradePanel, goUpgradePanel.transform.position + new Vector3 (1300f, 0f, 0f), 0.25f).setEase (LeanTweenType.easeInQuad);
			bIsUpgradePanelOpen = true;
		}

		//My shitty attempt at trying to make transitions from upgrade panel straight to recruitment panel and vice versa
//		if(guRecruitmentBuilding.IsSelected() && bIsUpgradePanelOpen)
//		{
//			LeanTween.move (goUpgradePanel, goUpgradePanel.transform.position + new Vector3 (-1300f, 0f, 0f), 0.25f).setEase (LeanTweenType.easeInQuad);
//			bIsUpgradePanelOpen = false;
//			LeanTween.move (goRecruitmentPanel, goRecruitmentPanel.transform.position + new Vector3 (0f, 1040f, 0f), 0.25f).setEase (LeanTweenType.easeInQuad);
//			bIsRecruitmentPanelOpen = true;
//		}

//		if(guBase1Building.IsSelected() || 
//		   guBase2Building.IsSelected() ||
//		   guBase3Building.IsSelected() && bIsRecruitmentPanelOpen)
//		{
//			LeanTween.move (goRecruitmentPanel, goRecruitmentPanel.transform.position + new Vector3 (0f, -1040f, 0f), 0.25f).setEase (LeanTweenType.easeInQuad);
//			bIsRecruitmentPanelOpen = false;
//			LeanTween.move (goUpgradePanel, goUpgradePanel.transform.position + new Vector3 (1300f, 0f, 0f), 0.25f).setEase (LeanTweenType.easeInQuad);
//			bIsUpgradePanelOpen = true;
//		}

	}

	public void CloseMenu() {
		if(bIsRecruitmentPanelOpen)
		{
			LeanTween.move (goRecruitmentPanel, goRecruitmentPanel.transform.position + new Vector3 (0f, -1040f, 0f), 0.25f).setEase (LeanTweenType.easeInQuad);
			bIsRecruitmentPanelOpen = false;
		}

		if(bIsUpgradePanelOpen)
		{
			LeanTween.move (goUpgradePanel, goUpgradePanel.transform.position + new Vector3 (-1300f, 0f, 0f), 0.25f).setEase (LeanTweenType.easeInQuad);
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

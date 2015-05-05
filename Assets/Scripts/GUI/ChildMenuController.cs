using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChildMenuController : GUI_Base {

	public UIAudioManager audioManager;
	public GameObject goRecruitmentPanel;
	public GameObject goUpgradePanel;
	static bool bIsRecruitmentPanelOpen = false;
	static bool bIsUpgradePanelOpen = false;
	static bool bPanelMoving;
	static BaseGameStructure currentOpenBase;
	public float fTimer = 0.35f;
	private UIMisc UIMisc;

	//Init
	void Start() {
		audioManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIAudioManager>();
		UIMisc = FindObjectOfType<UIMisc>();
		LeanTween.move (goRecruitmentPanel, new Vector2(Screen.width/2f, -(2f * Screen.height)), 0.01f).setEase (LeanTweenType.easeInQuad);
		LeanTween.move (goUpgradePanel, new Vector2(Screen.width/2f, 2f * Screen.height), 0.01f).setEase (LeanTweenType.easeInQuad);
	}

	void Update() 
	{
		if(bPanelMoving)
		{
			fTimer -= Time.deltaTime;
			if(fTimer <= 0f)
			{
				bPanelMoving = false;
			}
		}
		else
		{
			fTimer = 0.35f;
		}

		if(UIMisc.bIsGameOver && bIsRecruitmentPanelOpen)
		{
			LeanTween.move (goRecruitmentPanel, new Vector2(Screen.width/2f, -(2f * Screen.height)), 0.25f).setEase (LeanTweenType.easeInQuad);
			AudioSource.PlayClipAtPoint( audioManager.acCloseRecruitment, Camera.main.transform.position);
			bIsRecruitmentPanelOpen = false;
			bPanelMoving = true;
		}

		if(UIMisc.bIsGameOver && bIsUpgradePanelOpen)
		{
			LeanTween.move (goUpgradePanel, new Vector2(Screen.width/2f, 2f * Screen.height), 0.25f).setEase (LeanTweenType.easeInQuad);
			AudioSource.PlayClipAtPoint( audioManager.acCloseUpgrade, Camera.main.transform.position);
			bIsUpgradePanelOpen = false;
			bPanelMoving = true;
		}

	}

	public void OpenCloseUpgradeMenu(BaseGameStructure currentBase)
	{
		if(!bPanelMoving)
		{
			if(!bIsUpgradePanelOpen)
			{
				currentOpenBase = currentBase;
				if(!bIsRecruitmentPanelOpen)
				{
					LeanTween.move (goUpgradePanel, new Vector2(Screen.width/2f, Screen.height/2f), 0.25f).setEase (LeanTweenType.easeInQuad);
					AudioSource.PlayClipAtPoint( audioManager.acOpenUpgrade, Camera.main.transform.position);
					bIsUpgradePanelOpen = true;
					bPanelMoving = true;
				}
				else
				{
					LeanTween.move (goRecruitmentPanel, new Vector2(Screen.width/2f, -(2f * Screen.height)), 0.25f).setEase (LeanTweenType.easeInQuad);
					AudioSource.PlayClipAtPoint( audioManager.acSwitchingMenus, Camera.main.transform.position);
					bIsRecruitmentPanelOpen = false;
					LeanTween.move (goUpgradePanel, new Vector2(Screen.width/2f, Screen.height/2f), 0.25f).setEase (LeanTweenType.easeInQuad);
					bIsUpgradePanelOpen = true;
					bPanelMoving = true;
				}
			}
			else
			{
				if(currentOpenBase == currentBase)
				{
				    LeanTween.move (goUpgradePanel, new Vector2(Screen.width/2f, 2f * Screen.height), 0.25f).setEase (LeanTweenType.easeInQuad);
					AudioSource.PlayClipAtPoint( audioManager.acCloseUpgrade, Camera.main.transform.position);
					bIsUpgradePanelOpen = false;
					bPanelMoving = true;
				}
				else
				{
					currentOpenBase = currentBase;
					AudioSource.PlayClipAtPoint( audioManager.acChangingBase, Camera.main.transform.position);
				}
			}
		}
	}

	public void OpenCloseRecruitmentMenu()
	{
		if(!bPanelMoving)
		{
			if(!bIsRecruitmentPanelOpen)
			{
				if(!bIsUpgradePanelOpen)
				{
					LeanTween.move (goRecruitmentPanel, new Vector2(Screen.width/2f, Screen.height/2f), 0.25f).setEase (LeanTweenType.easeInQuad);
					AudioSource.PlayClipAtPoint( audioManager.acOpenRecruitment, Camera.main.transform.position);
					bIsRecruitmentPanelOpen = true;
					bPanelMoving = true;
				}
				else
				{
					LeanTween.move (goUpgradePanel, new Vector2(Screen.width/2f, 2f * Screen.height), 0.25f).setEase (LeanTweenType.easeInQuad);
					AudioSource.PlayClipAtPoint( audioManager.acSwitchingMenus, Camera.main.transform.position);
					bIsUpgradePanelOpen = false;
					LeanTween.move (goRecruitmentPanel, new Vector2(Screen.width/2f, Screen.height/2f), 0.25f).setEase (LeanTweenType.easeInQuad);
					bIsRecruitmentPanelOpen = true;
					bPanelMoving = true;
				}
			}
			else
			{
				LeanTween.move (goRecruitmentPanel, new Vector2(Screen.width/2f, -(2f * Screen.height)), 0.25f).setEase (LeanTweenType.easeInQuad);
				AudioSource.PlayClipAtPoint( audioManager.acCloseRecruitment, Camera.main.transform.position);
				bIsRecruitmentPanelOpen = false;
				bPanelMoving = true;
			}
		}
	}
}

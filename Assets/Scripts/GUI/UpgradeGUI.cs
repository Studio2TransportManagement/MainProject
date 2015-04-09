using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeGUI : MonoBehaviour {

	public baseStructure upgradingBase;

	private PlayerResources pPlayerResources;

	public bool bPanelActive;

	public Image IBaseName;
	public Sprite sBaseAlpha;
	public Sprite sBaseBravo;
	public Sprite sBaseCharlie;
	public Image tBaseHealth;
	public Text tCurrency;
	public Text tBaseHealthValue;
	public Image IIntegrityButton;
	public Sprite sIntegrityBtn1;
	public Sprite sIntegrityBtn2;
	public Sprite sMaxBtn3;
	public Image IIntegrityLevel;
	public Sprite sLvl1;
	public Sprite sLvl2;
	public Sprite sLvl3;
	public Text tIntegrityUpgradeCost;
	public Text tIntegrityInfo;
	public Image IWindowsButton;
	public Sprite sWindowsBtn1;
	public Sprite sWindowsBtn2;
	public Image IWindowsLvl;
	public Text tWindowsUpgradeCost;
	public Text tWindowsInfo;
	public Image ITrainsButton;
	public Sprite sTrainsBtn1;
	public Sprite sTrainsBtn2;
	public Image ITrainsLvl;
	public Text tTrainsUpgradeCost;
	public Text tTrainsInfo;

	// Use this for initialization
	void Awake () {
		pPlayerResources = FindObjectOfType<PlayerResources>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(upgradingBase)
		{
			if(upgradingBase.sBaseName == "Base Alpha")
			{
				IBaseName.overrideSprite = sBaseAlpha;
			}
			if(upgradingBase.sBaseName == "Base Bravo")
			{
				IBaseName.overrideSprite = sBaseBravo;
			}
			if(upgradingBase.sBaseName == "Base Charlie")
			{
				IBaseName.overrideSprite = sBaseCharlie;
			}

			tCurrency.text = "$ " + pPlayerResources.GetMoney() + "";
			tBaseHealth.fillAmount = upgradingBase.fHealthCurrent / upgradingBase.fHealthMax;
			tBaseHealthValue.text = "" + upgradingBase.iHealthCurrent + "/" + upgradingBase.fHealthMax + "";

			if(upgradingBase.iIntegrityLevel == 1)
			{
				IIntegrityButton.overrideSprite = sIntegrityBtn1;
				IIntegrityLevel.overrideSprite = sLvl1;
				tIntegrityUpgradeCost.color = new Color32(255, 255, 255, 255);
				tIntegrityInfo.text = "This will increase the integrity of this base by " + upgradingBase.iIntegrityUpgradeAmount + "."; 
			}
			if(upgradingBase.iIntegrityLevel == 2)
			{
				IIntegrityButton.overrideSprite = sIntegrityBtn2;
				IIntegrityLevel.overrideSprite = sLvl2;
				tIntegrityUpgradeCost.color = new Color32(255, 255, 255, 255);
				tIntegrityInfo.text = "This will increase the integrity of this base by " + upgradingBase.iIntegrityUpgradeAmount + ".";
			}
			if(upgradingBase.iIntegrityLevel == 3)
			{
				IIntegrityButton.overrideSprite = sMaxBtn3;
				IIntegrityLevel.overrideSprite = sLvl3;
				tIntegrityUpgradeCost.color = new Color32(0, 0, 0, 0);
				tIntegrityInfo.text = "You have levelled up this base's integrity to the maximum.";
			}
			tIntegrityUpgradeCost.text = "Cost = $" + upgradingBase.iIntegrityUpgradeCost + "";

			if(upgradingBase.iWindowLevel == 1)
			{
				IWindowsButton.overrideSprite = sWindowsBtn1;
				IWindowsLvl.overrideSprite = sLvl1;
				tWindowsUpgradeCost.color = new Color32(255, 255, 255, 255);
				tWindowsInfo.text = "This will increase the number of windows for units to shoot from by " + upgradingBase.iWindowsUpgradeAmount + ".";
			}
			if(upgradingBase.iWindowLevel == 2)
			{
				IWindowsButton.overrideSprite = sWindowsBtn2;
				IWindowsLvl.overrideSprite = sLvl2;
				tWindowsUpgradeCost.color = new Color32(255, 255, 255, 255);
				tWindowsInfo.text = "This will increase the number of windows for units to shoot from by " + upgradingBase.iWindowsUpgradeAmount + ".";
			}
			if(upgradingBase.iWindowLevel == 3)
			{
				IWindowsButton.overrideSprite = sMaxBtn3;
				IWindowsLvl.overrideSprite = sLvl3;
				tWindowsUpgradeCost.color = new Color32(0, 0, 0, 0);
				tWindowsInfo.text = "You have levelled up this base's window capacity to the maximum.";
			}
			tWindowsUpgradeCost.text = "Cost = $" + upgradingBase.iWindowUpgradeCost + "";

			if(upgradingBase.iTrainsLevel == 1)
			{
				ITrainsButton.overrideSprite = sTrainsBtn1;
				ITrainsLvl.overrideSprite = sLvl1;
				tTrainsUpgradeCost.color = new Color32(255, 255, 255, 255);
				tTrainsInfo.text = "This will increase the number of units that can go on this base's adjacent trains by " + upgradingBase.iCapacityUpgradeAmount + ".";
			}
			if(upgradingBase.iTrainsLevel == 2)
			{
				ITrainsButton.overrideSprite = sTrainsBtn2;
				ITrainsLvl.overrideSprite = sLvl2;
				tTrainsUpgradeCost.color = new Color32(255, 255, 255, 255);
				tTrainsInfo.text = "This will increase the number of units that can go on this base's adjacent trains by " + upgradingBase.iCapacityUpgradeAmount + ".";
			}
			if(upgradingBase.iTrainsLevel == 3)
			{
				ITrainsButton.overrideSprite = sMaxBtn3;
				ITrainsLvl.overrideSprite = sLvl3;
				tTrainsUpgradeCost.color = new Color32(0, 0, 0, 0);
				tTrainsInfo.text = "You have levelled up this base's train capacity to the maximum.";
			}
			tTrainsUpgradeCost.text = "Cost = $" + upgradingBase.iTrainsUpgradeCost + "";
		}
	}

	public void IntegrityUpgrade ()
	{
		if(pPlayerResources.GetMoney() >= upgradingBase.iIntegrityUpgradeCost &&
		   upgradingBase.iIntegrityLevel != 3)
		{
			pPlayerResources.SetMoney(pPlayerResources.GetMoney() - upgradingBase.iIntegrityUpgradeCost);
			upgradingBase.fHealthMax += upgradingBase.iIntegrityUpgradeAmount;
			upgradingBase.iIntegrityLevel += 1;
			upgradingBase.iIntegrityUpgradeCost += 100;
		}
	}

	public void WindowUpgrade ()
	{
		if(pPlayerResources.GetMoney() >= upgradingBase.iWindowUpgradeCost &&
		   upgradingBase.iWindowLevel != 3)
		{
			pPlayerResources.SetMoney(pPlayerResources.GetMoney() - upgradingBase.iWindowUpgradeCost);
			upgradingBase.iWindowLevel += 1;
			upgradingBase.UpgradeWindows();
			upgradingBase.iWindowUpgradeCost += 100;
		}
		
	}
	
	public void CapacityUpgrade ()
	{
		if(pPlayerResources.GetMoney() >= upgradingBase.iTrainsUpgradeCost &&
		   upgradingBase.iTrainsLevel != 3)
		{
			pPlayerResources.SetMoney(pPlayerResources.GetMoney() - upgradingBase.iTrainsUpgradeCost);
			upgradingBase.iCapacity += upgradingBase.iCapacityUpgradeAmount;
			upgradingBase.iTrainsLevel += 1;
			upgradingBase.iTrainsUpgradeCost += 100;
		}
	}
}
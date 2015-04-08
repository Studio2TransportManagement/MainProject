using UnityEngine;
using System.Collections;

public class UpgradeGUI : MonoBehaviour {

	public baseStructure upgradingBase;

	private PlayerResources pPlayerResources;
	private stats Camera;

	public bool bPanelActive;

	// Use this for initialization
	void Awake () {
		pPlayerResources = FindObjectOfType<PlayerResources>();
		Camera = FindObjectOfType<stats>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(upgradingBase)
		{
			Camera.tBaseName.text = "" + upgradingBase.sBaseName + " Upgrades";
			Camera.tBaseHealth.fillAmount = upgradingBase.fHealthCurrent / upgradingBase.fHealthMax;
			Camera.tBaseHealthValue.text = "" + upgradingBase.iHealthCurrent + "/" + upgradingBase.fHealthMax + "";
			Camera.tIntegrityLevel.text = "Level: " + upgradingBase.iIntegrityLevel + "";
			Camera.tIntegrityUpgradeCost.text = "Cost for next level = $" + upgradingBase.iIntegrityUpgradeCost + "";
			Camera.tWindowLevel.text = "Level: " + upgradingBase.iWindowLevel + "";
			Camera.tWindowUpgradeCost.text = "Cost for next level = $" + upgradingBase.iWindowUpgradeCost + "";
			Camera.tCapacityLevel.text = "Level: " + upgradingBase.iCapacityLevel + "";
			Camera.tCapacityUpgradeCost.text = "Cost for next level = $" + upgradingBase.iCapacityUpgradeCost + "";
		}
	}

	public void IntegrityUpgrade ()
	{
		if(pPlayerResources.GetMoney() >= upgradingBase.iIntegrityUpgradeCost &&
		   upgradingBase.iIntegrityLevel != 3)
		{
			pPlayerResources.SetMoney(pPlayerResources.GetMoney() - upgradingBase.iIntegrityUpgradeCost);
			upgradingBase.fHealthMax += 100f;
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
		if(pPlayerResources.GetMoney() >= upgradingBase.iCapacityUpgradeCost &&
		   upgradingBase.iCapacityLevel != 3)
		{
			pPlayerResources.SetMoney(pPlayerResources.GetMoney() - upgradingBase.iCapacityUpgradeCost);
			upgradingBase.iCapacity += 2;
			upgradingBase.iCapacityLevel += 1;
			upgradingBase.iCapacityUpgradeCost += 100;
		}
	}
}
using UnityEngine;
using System.Collections;

public class UpgradeGUI : MonoBehaviour {

	public baseStructure upgradingBase;

	private PlayerResources pPlayerResources;
	
	public bool bPanelActive;

	// Use this for initialization
	void Awake () {
		pPlayerResources = FindObjectOfType<PlayerResources>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	public void WindowUpgrade ()
	{
		if(pPlayerResources.GetMoney() >= upgradingBase.iWindowUpgradeCost &&
		   upgradingBase.iWindowLevel != 3)
		   {
		   	upgradingBase.UpgradeWindows();
		   }
		
	}
	
	public void CapacityUpgrade ()
	{
		
	}
}


//if(SelectionManager.l_goCurrentSelection.Count != 0)
//{
//	if(SelectionManager.l_goCurrentSelection[0].gameObject.name.StartsWith("Base "))
//	{
//		iHealthCurrent = (int) fHealthCurrent;
//		
//		GameStructure currentBase = SelectionManager.l_goCurrentSelection[0].GetComponent<GameStructure>();
//		stMainCamera.tBaseName.text = "" + currentBase.sBaseName + " Upgrades";
//		stMainCamera.tBaseHealth.fillAmount = currentBase.fHealthCurrent / currentBase.fHealthMax;
//		stMainCamera.tBaseHealthValue.text = "" + currentBase.iHealthCurrent + "/" + currentBase.fHealthMax + "";
//		stMainCamera.tIntegrityLevel.text = "Level: " + currentBase.iIntegrityLevel + "";
//		stMainCamera.tIntegrityUpgradeCost.text = "Cost for next level = $" + currentBase.iIntegrityUpgradeCost + "";
//		stMainCamera.tWindowLevel.text = "Level: " + currentBase.iWindowLevel + "";
//		stMainCamera.tWindowUpgradeCost.text = "Cost for next level = $" + currentBase.iWindowUpgradeCost + "";
//		stMainCamera.tCapacityLevel.text = "Level: " + currentBase.iCapacityLevel + "";
//		stMainCamera.tCapacityUpgradeCost.text = "Cost for next level = $" + currentBase.iCapacityUpgradeCost + "";
//	}
//}
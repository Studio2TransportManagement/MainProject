using UnityEngine;
using System.Collections;

public class GameStructure : MonoBehaviour, ISelectable {

	public bool bUnselectable {
		get;
		set;
	}
	
	public SelectionManager selectionManager {
		get;
		set;
	}

	public SelectionManager SelectionManager;
	public stats stMainCamera;

	public string sBaseName;

	public float fHealthMax;
	public float fHealthCurrent;

	public int iIntegrityLevel = 1;
	public int iIntegrityUpgradeCost = 50;
	public int iWindowLevel = 1;
	public int iWindows = 3;
	public int iWindowUpgradeCost = 50;
	public int iCapacityLevel = 1;
	public int iCapacity = 3;
	public int iCapacityUpgradeCost = 50;
	public int iHealthCurrent;

	// Use this for initialization
	void Awake () 
	{
		fHealthCurrent = fHealthMax;
		bUnselectable = false;
		selectionManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SelectionManager>();
		stMainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<stats>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		fHealthCurrent = Mathf.Clamp(fHealthCurrent, 0f, fHealthMax);
		iIntegrityLevel = Mathf.Clamp(iIntegrityLevel, 1, 3);
		iWindowLevel = Mathf.Clamp(iWindowLevel, 1, 3);
		iCapacityLevel = Mathf.Clamp(iCapacityLevel, 1, 3);

		if(SelectionManager.l_goCurrentSelection.Count != 0)
		{
			if(SelectionManager.l_goCurrentSelection[0].gameObject.name.StartsWith("Base "))
			{
				iHealthCurrent = (int) fHealthCurrent;
				
				GameStructure currentBase = SelectionManager.l_goCurrentSelection[0].GetComponent<GameStructure>();
				stMainCamera.tBaseName.text = "" + currentBase.sBaseName + " Upgrades";
				stMainCamera.tBaseHealth.fillAmount = currentBase.fHealthCurrent / currentBase.fHealthMax;
				stMainCamera.tBaseHealthValue.text = "" + currentBase.iHealthCurrent + "/" + currentBase.fHealthMax + "";
				stMainCamera.tIntegrityLevel.text = "Level: " + currentBase.iIntegrityLevel + "";
				stMainCamera.tIntegrityUpgradeCost.text = "Cost for next level = $" + currentBase.iIntegrityUpgradeCost + "";
				stMainCamera.tWindowLevel.text = "Level: " + currentBase.iWindowLevel + "";
				stMainCamera.tWindowUpgradeCost.text = "Cost for next level = $" + currentBase.iWindowUpgradeCost + "";
				stMainCamera.tCapacityLevel.text = "Level: " + currentBase.iCapacityLevel + "";
				stMainCamera.tCapacityUpgradeCost.text = "Cost for next level = $" + currentBase.iCapacityUpgradeCost + "";
			}
		}

		if(fHealthCurrent <= 0)
		{
			Application.LoadLevel(2);
		}

	}

	public bool IsSelected() {
		if (selectionManager.IsAlreadySelected(this.gameObject)) {
			//Debug.Log("<color=purple>" + this.gameObject.name + " is selected</color>");
			return true;
		}
		
		//Debug.Log("<color=purple>" + this.gameObject.name + " is NOT selected</color>");
		return false;
	}
}

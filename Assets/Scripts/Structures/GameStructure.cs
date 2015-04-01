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

	public string sBaseName;

	public float fHealthMax;
	public float fHealthCurrent;

	public int iIntegrityLevel = 1;
	public int iIntegrityUpgradeCost = 50;
	public int iWindowLevel = 1;
	public int iWindows = 4;
	public int iWindowUpgradeCost = 50;
	public int iCapacityLevel = 1;
	public int iCapacity = 3;
	public int iCapacityUpgradeCost = 50;
	public int iHealthCurrent;

	// Use this for initialization
	void Start () 
	{
		fHealthCurrent = fHealthMax;
		bUnselectable = false;
		selectionManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SelectionManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if(SelectionManager.l_goCurrentSelection.Count != 0)
		{
			if(SelectionManager.l_goCurrentSelection[0].gameObject.name.StartsWith("Base "))
			{
				iHealthCurrent = (int) fHealthCurrent;
				
				GameStructure currentBase = SelectionManager.l_goCurrentSelection[0].GetComponent<GameStructure>();
				Camera.main.GetComponent<stats>().tBaseName.text = "" + currentBase.sBaseName + " Upgrades";
				Camera.main.GetComponent<stats>().tBaseHealth.fillAmount = currentBase.fHealthCurrent / currentBase.fHealthMax;
				Camera.main.GetComponent<stats>().tBaseHealthValue.text = "" + currentBase.iHealthCurrent + "/" + currentBase.fHealthMax + "";
				Camera.main.GetComponent<stats>().tIntegrityLevel.text = "Level: " + currentBase.iIntegrityLevel + "";
				Camera.main.GetComponent<stats>().tIntegrityUpgradeCost.text = "Cost for next level = $" + currentBase.iIntegrityUpgradeCost + "";
				Camera.main.GetComponent<stats>().tWindowLevel.text = "Level: " + currentBase.iWindowLevel + "";
				Camera.main.GetComponent<stats>().tWindowUpgradeCost.text = "Cost for next level = $" + currentBase.iWindowUpgradeCost + "";
				Camera.main.GetComponent<stats>().tCapacityLevel.text = "Level: " + currentBase.iCapacityLevel + "";
				Camera.main.GetComponent<stats>().tCapacityUpgradeCost.text = "Cost for next level = $" + currentBase.iCapacityUpgradeCost + "";
			}
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

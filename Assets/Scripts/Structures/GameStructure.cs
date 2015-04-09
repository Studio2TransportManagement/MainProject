using UnityEngine;
using System.Collections;

public class GameStructure : MonoBehaviour {

	public string sBaseName;

	public float fHealthMax;
	public float fHealthCurrent;

	public int iIntegrityLevel = 1;
	public int iIntegrityUpgradeCost = 50;
	public int iIntegrityUpgradeAmount = 250;
	public int iWindowLevel = 1;
	public int iWindows = 3;
	public int iWindowUpgradeCost = 50;
	public int iWindowsUpgradeAmount = 1;
	public int iTrainsLevel = 1;
	public int iCapacity = 3;
	public int iTrainsUpgradeCost = 50;
	public int iCapacityUpgradeAmount = 2;
	public int iHealthCurrent;

	public ChildMenuController CMC;

	// Use this for initialization
	void Awake () 
	{
		fHealthCurrent = fHealthMax;
	}
	
	// Update is called once per frame
	void Update () 
	{
		fHealthCurrent = Mathf.Clamp(fHealthCurrent, 0f, fHealthMax);
		iIntegrityLevel = Mathf.Clamp(iIntegrityLevel, 1, 3);
		iWindowLevel = Mathf.Clamp(iWindowLevel, 1, 3);
		iTrainsLevel = Mathf.Clamp(iTrainsLevel, 1, 3);

		if(fHealthCurrent <= 0)
		{
			Application.LoadLevel(2);
		}

	}

	void OnMouseDown()
	{
		if(sBaseName == "Recruitment Building")
		{
			CMC.OpenCloseRecruitmentMenu();
		}
	}

}

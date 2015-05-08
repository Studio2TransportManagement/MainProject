using UnityEngine;
using System.Collections;

public class GameStructure : MonoBehaviour {

	public string sBaseName;

	public float fHealthMax;
	public float fHealthCurrent;

	public int iIntegrityLevel = 1;
	public int iIntegrityUpgradeCost = 150;
	public int iIntegrityUpgradeCost2 = 300;
	public int iIntegrityUpgradeAmount = 250;
	public int iIntegrityUpgradeAmount2 = 500;
	public int iWindowLevel = 1;
	public int iWindows = 3;
	public int iWindowUpgradeCost = 100;
	public int iTrainsLevel = 1;
	public int iCapacity = 3;
	public int iTrainsUpgradeCost = 100;
	public int iTrainsUpgradeCost2 = 200;
	public int iCapacityUpgradeAmount = 2;
	public int iCapacityUpgradeAmount2 = 5;

	public GameObject goBaseHealth75;
	public GameObject goBaseHealth50;
	public GameObject goBaseHealth25;

	public ChildMenuController CMC;
	public UIMisc UIMisc;

	// Use this for initialization
	void Awake () 
	{
		fHealthCurrent = fHealthMax;
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
		fHealthCurrent = Mathf.Clamp(fHealthCurrent, 0f, fHealthMax);
		iIntegrityLevel = Mathf.Clamp(iIntegrityLevel, 1, 3);
		iWindowLevel = Mathf.Clamp(iWindowLevel, 1, 3);
		iTrainsLevel = Mathf.Clamp(iTrainsLevel, 1, 3);



		if(fHealthCurrent <= fHealthMax &&
		   fHealthCurrent > (3f/4f) * fHealthMax)
		{
			goBaseHealth75.SetActive (false);
		}
		
		if(fHealthCurrent <= (3f/4f) * fHealthMax &&
		   fHealthCurrent > (1f/2f) * fHealthMax)
		{
			goBaseHealth75.SetActive (true);
			goBaseHealth50.SetActive (false);
		}
		
		if(fHealthCurrent <= (1f/2f) * fHealthMax &&
		   fHealthCurrent > (1f/4f) * fHealthMax)
		{
			goBaseHealth50.SetActive (true);
			goBaseHealth25.SetActive (false);
		}
		
		if(fHealthCurrent <= (1f/4f) * fHealthMax)
		{
			goBaseHealth25.SetActive (true);
		}

		if(fHealthCurrent <= 0)
		{
			UIMisc.GameOver();
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

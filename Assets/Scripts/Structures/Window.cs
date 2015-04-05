using UnityEngine;
using System.Collections;

public class Window : MonoBehaviour {

	public GameObject goOpenModel;
	public GameObject goClosedModel;

	public Transform tStandingPosition;

	bool bIsActive;
	bool bIsManned;
	bool bIsTargeted;
		
	GameObject goStationedSoldier;

	public bool BIsActive
	{
		get
		{
			return bIsActive;
		}
	}

	// Use this for initialization
	void Start () {

		bIsActive = false;
		bIsManned = false;
		bIsTargeted = false;

		goOpenModel.SetActive(false);
		goClosedModel.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.C))
		{
			debugSwitchWindowState();
		}
	}

	void debugSwitchWindowState()
	{
		bIsActive = !bIsActive;
		goOpenModel.SetActive(!goOpenModel.activeInHierarchy);
		goClosedModel.SetActive(!goOpenModel.activeInHierarchy);
	}

	public void ActivateWindow(){

		bIsActive = true;
		goOpenModel.SetActive(true);
		goClosedModel.SetActive(false);
	}

	public Transform ManWindow(GameObject soldier)
	{
		goStationedSoldier = soldier;
		bIsManned = true;

		return tStandingPosition;
	}

	public void LeaveWindow()
	{
		goStationedSoldier = null;
		bIsManned = false;
	}

	public bool CheckIfTargetable()
	{
		if(bIsActive && bIsManned && !bIsTargeted)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public GameObject TargetWindow()
	{
		bIsTargeted = true;

		return goStationedSoldier;
	}

	public void RemoveTarget()
	{
		bIsTargeted = false;
	}
}

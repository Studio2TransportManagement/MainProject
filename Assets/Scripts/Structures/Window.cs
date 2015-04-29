using UnityEngine;
using System.Collections;

public class Window : MonoBehaviour {

	public GameObject goOpenModel;
	public GameObject goClosedModel;

	public Transform tStandingPosition;

	public bool bIsActive = false;
//		get;
//		set;
//	}

	public bool bIsManned = false;
//		get;
//		set;
//	}

	public bool bIsTargeted = false;
		
	public PlayerUnit goStationedSoldier;
	public EnemyUnit euTargetingEnemy;

	// Use this for initialization
	void Awake() {
		goOpenModel.SetActive(false);
		goClosedModel.SetActive(true);
	}
	
	// Update is called once per frame
	void Update() {

	}

	void debugSwitchWindowState() {
		bIsActive = !bIsActive;
		goOpenModel.SetActive(!goOpenModel.activeInHierarchy);
		goClosedModel.SetActive(!goOpenModel.activeInHierarchy);
	}

	public void ActivateWindow() {
		bIsActive = true;
		goOpenModel.SetActive(true);
		goClosedModel.SetActive(false);
	}

	public void ManWindow(PlayerUnit soldier) {
		goStationedSoldier = soldier;
		bIsManned = true;
		goStationedSoldier.bManningWindow = true;
		goStationedSoldier.wMannedWindow = this;
//		return tStandingPosition;
	}

	public void LeaveWindow() {
		goStationedSoldier.bManningWindow = false;
		goStationedSoldier.wMannedWindow = null;
		goStationedSoldier = null;
		bIsManned = false;
	}

	public bool CheckIfTargetable() {
		if (bIsActive && bIsManned && !bIsTargeted) {
			return true;
		}
		else {
			return false;
		}
	}

	public PlayerUnit TargetWindow(EnemyUnit eu) {
		bIsTargeted = true;
		euTargetingEnemy = eu;
		return goStationedSoldier;
	}

	public void RemoveTarget() {
		bIsTargeted = false;
		euTargetingEnemy = null;
	}
}

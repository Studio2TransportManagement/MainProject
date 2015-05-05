using UnityEngine;
using System.Collections;

public class Window : MonoBehaviour {

	public GameObject goClosedModel;

	public Transform tStandingPosition;

	public bool bIsActive = false;
	public bool bIsManned = false;
	public bool bIsTargeted = false;
		
	public PlayerUnit goStationedSoldier;
	public EnemyUnit euTargetingEnemy;

	public GameObject Rifle;
	public GameObject Bazooka;

	// Use this for initialization
	void Awake() {
		goClosedModel.SetActive(true);
		Rifle.SetActive(false);
		Bazooka.SetActive(false);
	}
	
	// Update is called once per frame
	void Update() {

	}

	public void ActivateWindow() {
		bIsActive = true;
		goClosedModel.SetActive(false);
	}

	public void ManWindow(PlayerUnit soldier) {
		goStationedSoldier = soldier;
		bIsManned = true;
		goStationedSoldier.bManningWindow = true;
		goStationedSoldier.wMannedWindow = this;

	}

	public void ActivateWeapons() {
		if(goStationedSoldier.SollyType == SOLDIER_TYPE.GUNNER) {
			Rifle.SetActive(true);
		}
		if(goStationedSoldier.SollyType == SOLDIER_TYPE.HEAVY) {
			Bazooka.SetActive(true);
		}
	}

	public void LeaveWindow() {
		goStationedSoldier.bManningWindow = false;
		goStationedSoldier.wMannedWindow = null;
		if(goStationedSoldier.SollyType == SOLDIER_TYPE.GUNNER) {
			Rifle.SetActive(false);
		}
		if(goStationedSoldier.SollyType == SOLDIER_TYPE.HEAVY) {
			Bazooka.SetActive(false);
		}

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

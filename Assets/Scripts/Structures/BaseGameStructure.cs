using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseGameStructure : GameStructure {

	public List<Window> l_windows;
	public bool PanelOpen;
	public UpgradeGUI UpgradeUI;
	public GameObject goAlertImage;
	public TrainStation tsLeftStation;
	public TrainStation tsRightStation;
	public List<EnemyUnit> l_euAttackers;

	public bool bAlert;

	void Awake() {
		l_windows = new List<Window>();
		l_windows.AddRange(GetComponentsInChildren<Window>());
		StaticGameStructures.bases.Add(this);

	}

	void Start () {
		l_euAttackers = new List<EnemyUnit> ();
		PanelOpen = false;
		UpgradeWindows();
	}

	protected override void Update() {
		base.Update();
		if(bAlert) {
			if(l_euAttackers.Count <= 0) {
				bAlert =false;
			}
			goAlertImage.gameObject.SetActive(true);
		}

		if(!bAlert) {
			goAlertImage.SetActive(false);
		}
	}

	//Allows PlayerUnits to check the base for an available window before moving to man it (eg. if(CheckIfWindowAvailable){targetWindow = GetAvailableOpenWindow() })
	public bool CheckIfWindowAvailable(){
		foreach (Window window in l_windows) 
		{
			if (window.bIsActive && !window.bIsManned) {
				return true;
			}
		}
		return false;
	}

	//Use after checking for open window, explained above, helps to avoid targeting a Null window
	public Window GetAvailableOpenWindow() {
		foreach (Window window in l_windows) {
			if (window.bIsActive && !window.bIsManned) {
				return window;
			}
		}
		Debug.Log("Get Window Failed, check if window is available before getting");
		return null;
	}

	//Same as for above, but for the enemies to determine manned windows, rather than unmanned
	public bool CheckForTargetableWindow(){
		foreach (Window window in l_windows) {
			if (window.CheckIfTargetable()) {
				return true;
			}
		}
		return false;
	}

	public Window TargetAvailableOpenWindow() {
		foreach (Window window in l_windows) {
			if (window.bIsActive && window.bIsManned) {
				return window;
			}
		}
		return null;
	}

	public void ModifyCurrentIntegrity(float amount) {
		if (amount > 0) {
			fHealthCurrent += amount;
		}
		if (amount < 0) {
			if (!bAlert) {
				bAlert = true;
			}
			fHealthCurrent += amount;
		}
	}

	public void UpgradeIntegrity() {
		//Oh geez this isn't done
		if (this.iIntegrityLevel < 3) {
			iIntegrityLevel++;
		}
	}

	public void UpgradeTrains() {
		//Oh god this needs doing
		if (this.iTrainsLevel < 3) {
			iTrainsLevel++;
		}
	}
	
	public void getThisBase() {
		UpgradeUI.upgradingBase = this;
		CMC.OpenCloseUpgradeMenu(UpgradeUI.upgradingBase);
	}

	public void CloseButton() {
		CMC.OpenCloseUpgradeMenu(UpgradeUI.upgradingBase);
	}
	
	public void UpgradeWindows() {
		if (iWindowLevel < 5) {
			iWindowLevel++;
		}
		ActivateWindows();
	}

	public void ActivateWindows() {
		//Base windows plus upgrade level
		int j = iWindowLevel + 2;
		for (int i = 0; i < j; i++) {
			l_windows[i].ActivateWindow();
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "enemy-unit") {
			if (!l_euAttackers.Contains (other.GetComponent<EnemyUnit>())) {
				l_euAttackers.Add(other.GetComponent<EnemyUnit>());	
			}
		}
	}

}

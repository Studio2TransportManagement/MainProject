using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseGameStructure : GameStructure {

	public List<Window> l_windows;
	public bool PanelOpen;
	public UpgradeGUI UpgradeUI;
//	public GameObject goAlertImage;
	public TrainStation tsLeftStation;
	public TrainStation tsRightStation;
	public List<Transform> l_tWanderPoints;
	public GameObject goStockpile;

	public List<EnemyUnit> l_euAttackers;
	private List<GameObject> l_goAllply;
	private List<GameUnit> l_guInjuredply;

	public bool bAlert;

	void Awake() {
		l_windows = new List<Window>();
		l_windows.AddRange(GetComponentsInChildren<Window>());
		StaticGameStructures.bases.Add(this);
	}

	void Start() {
		l_euAttackers = new List<EnemyUnit>();
		l_goAllply = new List<GameObject>();
		l_guInjuredply = new List<GameUnit>();
		PanelOpen = false;
		ActivateWindows();
	}

	protected override void Update() {
		ActivateWindows();
		base.Update();
		if (bAlert) {
			if (l_euAttackers.Count == 0) {
				bAlert = false;
			}
//			goAlertImage.gameObject.SetActive(true);
		}

		if (!bAlert) {
//			goAlertImage.SetActive(false);
		}
	}

	//Allows PlayerUnits to check the base for an available window before moving to man it (eg. if(CheckIfWindowAvailable){targetWindow = GetAvailableOpenWindow() })
	public bool CheckIfWindowAvailable() {
		foreach (Window window in l_windows) {
			if (window.bIsActive && !window.bIsManned) {
				return true;
			}
		}
		return false;
	}

	public List<PlayerUnit> GetAllUnitsInBase() {
		l_goAllply.Clear();
		l_goAllply.AddRange(GameObject.FindGameObjectsWithTag("player-unit"));
		List<PlayerUnit> allunits = new List<PlayerUnit>();
		PlayerUnit currentunit = null;

		foreach (GameObject go in l_goAllply) {
			currentunit = go.GetComponent<PlayerUnit>();
			if (currentunit != null) {
				if (currentunit.goTargetBase == this) {
					allunits.Add(currentunit);
				}
			}
		}

		return allunits;
	}

	public List<GameUnit> GetInjuredUnitsInBase() {
		GetAllUnitsInBase();
		l_guInjuredply.Clear();

		GameUnit guTemp = null;

		foreach (GameObject curunit in l_goAllply) {
			guTemp = curunit.GetComponent<PlayerUnit>();
			if (guTemp.fHealthCurrent > 0 && guTemp.fHealthCurrent < guTemp.fHealthMax) {
				l_guInjuredply.Add(guTemp);
			}
		}

		return l_guInjuredply;
	}

	public List<Window> GetMannedWindows() {
		List<Window> lManned = new List<Window>();
		foreach (Window win in l_windows) {
			if (win.bIsManned && win.bIsActive) {
				lManned.Add(win);
			}
		}
		return lManned;
	}

	//Use after checking for open window, helps to avoid targeting a Null window
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
	public bool CheckForTargetableWindow() {
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

	public void DamageBase(float amount, GameUnit attacker) {
		if(attacker.SollyType == SOLDIER_TYPE.ENEMY_TANK) {
			if (!bAlert) {
				bAlert = true;
			}
			fHealthCurrent -= amount * 10;
			List<PlayerUnit> tempPU = new List<PlayerUnit>();
			tempPU = GetAllUnitsInBase();

			foreach(PlayerUnit pu in tempPU) {
				pu.DamageUnit(amount, attacker);
			}
		}

		else {
			if (!bAlert) {
				bAlert = true;
			}
			fHealthCurrent -= amount;
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
		if (iWindowLevel < 3) {
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

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "enemy-unit") {
			EnemyUnit tempEU = other.GetComponent<EnemyUnit>();
			if (!l_euAttackers.Contains(tempEU)) {
				l_euAttackers.Add(tempEU);	
			}
		}
	}

	public void Repair(float amount) {
		fHealthCurrent += amount;
		Mathf.Clamp(fHealthCurrent, 0, fHealthMax);
	}

}

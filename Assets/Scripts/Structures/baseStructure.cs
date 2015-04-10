using UnityEngine;
using System.Collections;

public class BaseStructure : GameStructure {

	Window[] windows;
	public bool PanelOpen;
	public UpgradeGUI UpgradeUI;

	public bool bAlert;

	public Window[] Windows {
		get;
		set;
	}

	void Awake() {
		windows = GetComponentsInChildren<Window>();
		staticStructures.bases.Add(this);
	}

	void Start () {
		PanelOpen = false;
		UpgradeWindows();
	}

	public Window GetAvailableOpenWindow() {
		foreach (Window window in windows) {
			if (window.bIsActive && !window.bIsManned) {
				return window;
			}
		}
		Debug.Log("Get Window Failed, check if window is available before getting");
		return null;
	}

	public void UpgradeIntegrity() {
		
	}
	
	public void getThisBase() {
		UpgradeUI.upgradingBase = this;
		CMC.OpenCloseUpgradeMenu (UpgradeUI.upgradingBase);
	}
	
	public void UpgradeWindows() {
		//Base windows plus upgrade level
		int j = iWindowLevel + 2;
		for (int i = 0; i < j; i++) {
			windows[i].ActivateWindow();
		}
	}

}

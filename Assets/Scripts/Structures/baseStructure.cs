﻿using UnityEngine;
using System.Collections;

public class baseStructure : GameStructure {

	Window[] windows;
	public bool PanelOpen;
	public UpgradeGUI UpgradeUI;
	public ChildMenuController CMC;

	public bool bAlert;

	public Window[] Windows
	{
		get
		{
			return windows;
		}
	}

	void Awake()
	{
		windows = GetComponentsInChildren<Window>();
		staticStructures.bases.Add(this);

	}

	void Start () 
	{
		PanelOpen = false;
		UpgradeWindows();
	}

	public Window GetAvailableOpenWindow()
	{
		foreach(Window window in windows)
		{
			if(window.BIsActive && !window.BIsManned)
			{
				return window;
			}
		}
		Debug.Log ("Get Window Failed, check if window is available before getting");
		return null;
	}

	public void UpgradeIntegrity()
	{
		
	}
	
	public void getThisBase()
	{
		UpgradeUI.upgradingBase = this;
		if (!PanelOpen) 
		{
			CMC.OpenMenu();
			PanelOpen = true;
		}
		if (PanelOpen) 
		{
			CMC.CloseMenu();
			PanelOpen = false;
		}

	}
	
	public void UpgradeWindows()
	{
		switch(iWindowLevel)
		{
		case 1:
			for(int i = 0; i < 3; i++)
			{
				windows[i].ActivateWindow();
			}
			break;
		case 2:
			for(int i = 0; i < 4; i++)
			{
				if(!windows[i].BIsActive)
				{
					windows[i].ActivateWindow();
				}
			}
			break;
		case 3:
			for(int i = 0; i < 5; i++)
			{
				if(!windows[i].BIsActive)
				{
					windows[i].ActivateWindow();
				}
			}
			break;
		default:
			Debug.Log ("Max Windows!");
			break;
		}
	}

}

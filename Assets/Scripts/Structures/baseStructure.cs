using UnityEngine;
using System.Collections;

public class baseStructure : GameStructure {

	Window[] windows;

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
		ActivateWindowsByLevel();
	}

	public void ActivateWindowsByLevel()
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

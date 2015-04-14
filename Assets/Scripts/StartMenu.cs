using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

	public GameObject goMainPanel;
	public GameObject goInstructionsPanel;

	public void Play ()
	{
//		goInstructionsPanel.SetActive (true);
//		goMainPanel.SetActive (false);
		Application.LoadLevel (1);
	}

	public void StartGame ()
	{
		Application.LoadLevel (1);
	}

	public void Exit ()
	{
		Application.Quit ();
	}

}

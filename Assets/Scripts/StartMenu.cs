using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

	public void Play ()
	{
		Application.LoadLevel (1);
	}

	public void Instructions ()
	{

	}

	public void Exit ()
	{
		Application.Quit ();
	}

}

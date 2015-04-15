using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NamePrinter : MonoBehaviour {

	public Text tMemorialWallNames;
	public NameSaver MWNameSaver;

	void Start ()
	{
		//MWNameSaver = GameObject.FindGameObjectWithTag("NameSaver").GetComponent<NameSaver>();
	}

	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.T))
		{
			tMemorialWallNames.text = "Bob";
		}
	}
}

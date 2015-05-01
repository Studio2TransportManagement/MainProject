using UnityEngine;
using System.Collections;

public class Workbench : MonoBehaviour {

	private BaseGameStructure CurrentBase;
	public GameObject goHighlightedWorkbench;

	void Start ()
	{
		CurrentBase = gameObject.GetComponentInParent<BaseGameStructure> ();
	}

	void OnMouseEnter() {
		goHighlightedWorkbench.SetActive (true);
	}
	
	void OnMouseExit() {
		goHighlightedWorkbench.SetActive (false);
	}

	void OnMouseDown ()
	{
		CurrentBase.getThisBase ();
	}
}

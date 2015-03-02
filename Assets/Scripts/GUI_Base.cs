using UnityEngine;
using System.Collections;

public class GUI_Base : MonoBehaviour {

	//
	public virtual void OnSelected() {
		Debug.Log("GUI_Base selected");
	}
	
	//
	public virtual void OnDeselected() {
		Debug.Log("GUI_Base deselected");
	}
}

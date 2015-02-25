using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChildMenuController : MonoBehaviour {

	RaycastHit hit;
	Ray rRay;

	public Button[] a_buttons;

	void Start() {
		hit = new RaycastHit();

		a_buttons = GetComponentsInChildren<Button>();
	}

	//Update 
	void Update () 
	{

		if (Input.GetMouseButtonDown(0)) 
		{
			rRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast(rRay, out hit, 10000))
			{
				if (hit.transform.tag == "building") 
				{
					Debug.Log ("You shit kent");

					foreach (Button but in a_buttons) {
						but.GetComponent<GUIClickMenu>().bMenuOn = true;
					}

				}
			}
		}

		if (Input.GetMouseButtonDown (1)) {
			foreach (Button but in a_buttons) {
				but.GetComponent<GUIClickMenu>().bMenuOn = false;
			}
		}
		//Detect Tag Underneath mouse position
		//On click, make the menu pop up.
	}
}

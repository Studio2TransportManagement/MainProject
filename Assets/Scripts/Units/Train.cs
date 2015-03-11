using UnityEngine;
using System.Collections;

public class Train : GameUnit {
	
	GameUnit[] a_guPassengers;

	// Use this for initialization
	void Start () {
		bUnselectable = false;
		selectionManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SelectionManager>();
	}

	// Update is called once per frame
	void Update () {
		if (this.IsSelected()) {
			this.renderer.material.color = Color.red;
		}
		else {
			this.renderer.material.color = Color.white;
		}
	}
}

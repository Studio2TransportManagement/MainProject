using UnityEngine;
using System.Collections;

public class GameUnit : MonoBehaviour, ISelectable {

	public bool bUnselectable {
		get;
		set;
	}

	public SelectionManager selectionManager {
		get;
		set;
	}

	public string sUnitName;

	public float fHealthMax;
	public float fHealthCurrent;
	public float fRange;
	public float fFireRate;
	
	public int iAmmo;

	// Use this for initialization
	void Start () {
		bUnselectable = false;
		selectionManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SelectionManager>();
	}

	public bool IsSelected() {
		if (selectionManager.GetSelection() == this) {
			Debug.Log("<color=purple>" + this.gameObject.name + " is selected</color>");
			return true;
		}

		return false;
	}

	// Update is called once per frame
	void Update () {
		//
	}
}

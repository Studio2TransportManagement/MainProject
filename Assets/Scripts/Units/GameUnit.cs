using UnityEngine;
using UnityEngine.UI;
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

	public GameObject goHealthBar;
	public GameObject goHealthInstance;
	
	public int iAmmo;

	// Use this for initialization
	void Start () {
		bUnselectable = false;
		selectionManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SelectionManager>();
		goHealthInstance = Instantiate (goHealthBar) as GameObject;
		goHealthInstance.transform.SetParent (GameObject.Find ("Canvas").transform, false);
		//goHealthInstance.SetActive (false);
	}

	public bool IsSelected() {
		if (selectionManager.IsAlreadySelected(this.gameObject)) {
			//Debug.Log("<color=purple>" + this.gameObject.name + " is selected</color>");
			return true;
		}

		//Debug.Log("<color=purple>" + this.gameObject.name + " is NOT selected</color>");
		return false;
	}

	// Update is called once per frame
	void Update () {

		Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position + new Vector3 (0f, 0.25f, 0f));
		
		goHealthInstance.GetComponent<RectTransform>().anchoredPosition = screenPoint - GameObject.Find ("Canvas").transform.GetComponent<RectTransform>().sizeDelta / 2f;
		goHealthInstance.GetComponentInChildren<Text> ().text = sUnitName;

	}
}

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
	public int iIntegrityLevel = 1;
	public int iIntegrityUpgradeCost = 50;
	public int iWindowLevel = 1;
	public int iWindows = 4;
	public int iWindowUpgradeCost = 50;
	public int iCapacityLevel = 1;
	public int iCapacity = 3;
	public int iCapacityUpgradeCost = 50;

	public SelectionManager SelectionManager;
	public GameObject goHealthBar;
	public GameObject goHealthInstance;
	
	public int iAmmo;

	// Use this for initialization
	void Start () {
		fHealthCurrent = fHealthMax;
		bUnselectable = false;
		selectionManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SelectionManager>();
		if(goHealthBar != null)
		{
			goHealthInstance = Instantiate (goHealthBar) as GameObject;
			goHealthInstance.transform.SetParent (GameObject.Find ("Canvas").transform, false);
			goHealthInstance.transform.SetAsFirstSibling ();
			goHealthInstance.SetActive (false);
		}
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
		fHealthCurrent = Mathf.Clamp(fHealthCurrent, 0f, fHealthMax);

		if(goHealthInstance == null && SelectionManager != null && SelectionManager.goCurrentObject != null && SelectionManager.goCurrentObject.name == sUnitName)
		{
			int iHealthCurrent = (int) fHealthCurrent;
			
			Camera.main.GetComponent<stats>().tBaseName.text = "" + sUnitName + " Upgrades";
			Camera.main.GetComponent<stats>().tBaseHealth.fillAmount = fHealthCurrent / fHealthMax;
			Camera.main.GetComponent<stats>().tBaseHealthValue.text = "" + iHealthCurrent + "/" + fHealthMax + "";
			Camera.main.GetComponent<stats>().tIntegrityLevel.text = "Level: " + iIntegrityLevel + "";
			Camera.main.GetComponent<stats>().tIntegrityUpgradeCost.text = "Cost for next level = $" + iIntegrityUpgradeCost + "";
			Camera.main.GetComponent<stats>().tWindowLevel.text = "Level: " + iWindowLevel + "";
			Camera.main.GetComponent<stats>().tWindowUpgradeCost.text = "Cost for next level = $" + iWindowUpgradeCost + "";
			Camera.main.GetComponent<stats>().tCapacityLevel.text = "Level: " + iCapacityLevel + "";
			Camera.main.GetComponent<stats>().tCapacityUpgradeCost.text = "Cost for next level = $" + iCapacityUpgradeCost + "";
		}

		if(goHealthInstance != null)
		{
			if(IsSelected())
			{
				goHealthInstance.SetActive (true);
			}
			else
			{
				goHealthInstance.SetActive (false);
			}

			if(goHealthInstance.activeInHierarchy)
			{
				Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position + new Vector3 (0f, 1.50f, 0f));

				goHealthInstance.GetComponentsInChildren<Image>()[1].fillAmount = fHealthCurrent / fHealthMax;
				goHealthInstance.GetComponent<RectTransform>().anchoredPosition = screenPoint - GameObject.Find ("Canvas").transform.GetComponent<RectTransform>().sizeDelta / 2f;
				goHealthInstance.GetComponentInChildren<Text> ().text = sUnitName;
			}
		}
	}
}

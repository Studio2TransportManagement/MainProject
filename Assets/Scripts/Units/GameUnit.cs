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
	public SOLDIER_TYPE SollyType = SOLDIER_TYPE.NONE;

	public bool isHovering = false;

	public SelectionManager SelectionManager;
	public NameSaver NameSaver;
	public GameObject goHealthBar;
	public GameObject goHealthInstance;

	public Color32 colHover;
	public Color32 colSelected;
	public Color32 colDeselected;

	public NavMeshAgent navAgent;
	
	public int iAmmo;

	// Use this for initialization
	void Start () {
		fHealthCurrent = fHealthMax;
		bUnselectable = false;
		selectionManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SelectionManager>();
		NameSaver = GameObject.FindGameObjectWithTag ("NameSaver").GetComponent<NameSaver>();
		if(goHealthBar != null)
		{
			goHealthInstance = Instantiate (goHealthBar) as GameObject;
			goHealthInstance.transform.SetParent (GameObject.Find ("Main Canvas").transform, false);
			goHealthInstance.transform.SetAsFirstSibling ();
			goHealthInstance.SetActive (false);
		}
		navAgent = this.gameObject.GetComponent<NavMeshAgent>();
		Debug.Log ("Unit Initiated");
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


		if(goHealthInstance != null)
		{
			if(IsSelected())
			{
				goHealthInstance.SetActive (true);
				gameObject.GetComponentInChildren<SpriteRenderer>().color = colSelected;
			}
			else
			{
				if(!isHovering)
				{
					goHealthInstance.SetActive (false);
					gameObject.GetComponentInChildren<SpriteRenderer>().color = colDeselected;
				}
				else
				{
					gameObject.GetComponentInChildren<SpriteRenderer>().color = colHover;
				}
			}

			if(goHealthInstance.activeInHierarchy)
			{
				Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position + new Vector3 (0f, 1.50f, 0f));

				goHealthInstance.GetComponentsInChildren<Image>()[1].fillAmount = fHealthCurrent / fHealthMax;
				goHealthInstance.GetComponent<RectTransform>().anchoredPosition = screenPoint - GameObject.Find ("Main Canvas").transform.GetComponent<RectTransform>().sizeDelta / 2f;
				goHealthInstance.GetComponentInChildren<Text> ().text = sUnitName;
			}
		}

		if(fHealthCurrent <= 0)
		{
			selectionManager.RemoveDeadUnitIfSelected(gameObject);
			NameSaver.l_guDeadUnitNames.Add (sUnitName);
			//Delay death until death animation has completed and then proceed to play slain message and delete player and correpsonding health bar.
			//if(deathAnimationHasFinished)
			//Do following functions.
			Camera.main.GetComponent<stats>().tSlainMessagePrintToUI(sUnitName);
			Destroy(goHealthInstance);
			Destroy(gameObject);
		}

	}

	void OnMouseEnter ()
	{
		isHovering = true;
		goHealthInstance.SetActive (true);
		if(!IsSelected())
		{
			gameObject.GetComponentInChildren<SpriteRenderer>().color = colHover;
		}
	}

	void OnMouseExit ()
	{
		isHovering = false;
		goHealthInstance.SetActive (false);
		if(!IsSelected())
		{
			gameObject.GetComponentInChildren<SpriteRenderer>().color = colDeselected;
		}
	}
}

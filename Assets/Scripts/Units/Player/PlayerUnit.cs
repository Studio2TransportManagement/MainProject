using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerUnit : GameUnit {

	public string sUnitName;

	protected FSM_Core<PlayerUnit> FSM;

	public SOLDIER_TYPE SollyType = SOLDIER_TYPE.NONE;
	
	public bool isHovering = false;
	
	public SelectionManager SelectionManager;
	public NameSaver nameSaver;
	public GameObject goHealthBar;
	public GameObject goHealthInstance;
	
	public Color32 colHover;
	public Color32 colSelected;
	public Color32 colDeselected;

	public bool bManningWindow;
	public bool bInTransit;

	private float fTimer = 1.75f;

	private SpriteRenderer spriteRenderer;



	// Use this for initialization
	protected override void Start() {
		base.Start();

		bInTransit = false;
		bUnselectable = false;
		nameSaver = GameObject.FindGameObjectWithTag("NameSaver").GetComponent<NameSaver>();
		goHealthInstance = Instantiate(goHealthBar) as GameObject;
		goHealthInstance.transform.SetParent(GameObject.Find("Main Canvas").transform, false);
		goHealthInstance.transform.SetAsFirstSibling();
		goHealthInstance.SetActive(false);
		spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
		//Debug.Log("PlayerUnit Initialised");

	}

	// Update is called once per frame
	protected override void Update() {
		base.Update();
		if (FSM != null) {
			FSM.Update();
		}
		SelectionCircle();
	}

	protected void SelectionCircle() {
		fHealthCurrent = Mathf.Clamp(fHealthCurrent, 0f, fHealthMax);
		
		if (goHealthInstance != null) {
			if (IsSelected()) {
				goHealthInstance.SetActive (true);
				spriteRenderer.color = colSelected;
			}
			else {
				if (!isHovering) {
					goHealthInstance.SetActive (false);
					spriteRenderer.color = colDeselected;
				}
				else {
					spriteRenderer.color = colHover;
				}
			}
			
			if (goHealthInstance.activeInHierarchy) {
				Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position + new Vector3 (0f, 1.50f, 0f));
				
				goHealthInstance.GetComponentsInChildren<Image>()[1].fillAmount = fHealthCurrent / fHealthMax;
				goHealthInstance.GetComponent<RectTransform>().anchoredPosition = screenPoint - GameObject.Find ("Main Canvas").transform.GetComponent<RectTransform>().sizeDelta / 2f;
				goHealthInstance.GetComponentInChildren<Text>().text = sUnitName;
			}
		}
	}

	protected override void UnitStopFlashing() {
		SkinnedMeshRenderer UnitsMesh = gameObject.GetComponentInChildren<SkinnedMeshRenderer> ();
		UnitsMesh.enabled = true;
	}

	protected override void UnitFlashing() {
		SkinnedMeshRenderer UnitsMesh = gameObject.GetComponentInChildren<SkinnedMeshRenderer> ();
		fTimer -= Time.deltaTime;
		
		if(fTimer <= 1.5f)
		{
			UnitsMesh.enabled = false;
		}
		if(fTimer <= 1.25f)
		{
			UnitsMesh.enabled = true;
		}
		if(fTimer <= 1.0f)
		{
			UnitsMesh.enabled = false;
		}
		if(fTimer <= 0.75f)
		{
			UnitsMesh.enabled = true;
		}
		if(fTimer <= 0.5f)
		{
			UnitsMesh.enabled = false;
		}
		if(fTimer <= 0.25f)
		{
			UnitsMesh.enabled = true;
		}
		if(fTimer <= 0f)
		{
			UnitsMesh.enabled = false;
			fTimer = 1.75f;
		}
	}

	protected override void KillUnit() {
		if (bManningWindow) {
			wMannedWindow.LeaveWindow();
		}
		selectionManager.RemoveDeadUnitIfSelected(this.gameObject);
		nameSaver.l_sDeadUnitNames.Add(sUnitName);
		//make sure if the unit dies at a window, we stop manning it

		//Delay death until death animation has completed and then proceed to play slain message and delete player and correpsonding health bar.
		//if(deathAnimationHasFinished)
		//Do following functions.
		Camera.main.GetComponent<UIMisc>().tSlainMessagePrintToUI(sUnitName);
		Destroy(goHealthInstance);
		Destroy(gameObject);
	}

	void OnMouseEnter() {
		isHovering = true;
		goHealthInstance.SetActive(true);
		if(!IsSelected()) {
			gameObject.GetComponentInChildren<SpriteRenderer>().color = colHover;
		}
	}
	
	void OnMouseExit() {
		isHovering = false;
		goHealthInstance.SetActive(false);
		if(!IsSelected()) {
			gameObject.GetComponentInChildren<SpriteRenderer>().color = colDeselected;
		}
	}

	public void ChangeState(FSM_State<PlayerUnit> gu) {
		FSM.ChangeState(gu);
	}

	public void ReturnToLastState(){
		FSM.ReturnToLastState();
	}

	public string GetStateName() {
		return this.FSM.GetStateName();
	}

	protected BaseGameStructure GetCurrentBase() {
		RaycastHit hit = new RaycastHit();
		Ray ray = new Ray(this.transform.position, Vector3.down);
		if (Physics.Raycast(ray, out hit, 10f, LayerMask.GetMask("building"))) {
			if (hit.transform.gameObject.tag == "building") {
				return this.goTargetBase = hit.transform.gameObject.GetComponent<BaseGameStructure>();
			}
		}
		Debug.Log ("Unit not detecting base");
		return null;
	}

	public bool IsFSMInitialised() {
		if (this.FSM != null) {
			return true;
		}

		return false;
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerUnit : GameUnit {
	public Vector3 NavDest;

	public string sUnitName;

	protected FSM_Core<PlayerUnit> FSM;

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

	private SpriteRenderer spriteRenderer;

	private bool bAtNavTargetPoint;
	public Vector3 vNavTarget;
	private PlayerResources pPlayerResources;

	ParticleEmitter peActionEffect;

	void Awake() {
		pPlayerResources = FindObjectOfType<PlayerResources>();
	}

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

		this.goTargetBase = GetCurrentBase();
		//Debug.Log("PlayerUnit Initialised");

	}

	// Update is called once per frame
	protected override void Update() {
		base.Update();
		NavDest = navAgent.destination;
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
				Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position + new Vector3(0f, 1.50f, 0f));
				
				goHealthInstance.GetComponentsInChildren<Image>()[1].fillAmount = fHealthCurrent / fHealthMax;
				goHealthInstance.GetComponent<RectTransform>().anchoredPosition = screenPoint - GameObject.Find("Main Canvas").transform.GetComponent<RectTransform>().sizeDelta / 2f;
				goHealthInstance.GetComponentInChildren<Text>().text = sUnitName;
			}
		}
	}

	protected override void KillUnit() {
		bStartedDying = true;
		aAnimator.SetBool("bIsDying", true);
		if (bManningWindow) {
			wMannedWindow.LeaveWindow();
		}
		selectionManager.RemoveDeadUnitIfSelected(this.gameObject);
		nameSaver.l_sDeadUnitNames.Add(sUnitName);
		pPlayerResources.iTotalRecruits--;
		Camera.main.GetComponent<UIMisc>().tSlainMessagePrintToUI(sUnitName);
		if(!asAudioSource.isPlaying) {
			asAudioSource.clip = uaUnitAudio.acDying;
			asAudioSource.volume = 0.5f;
			asAudioSource.Play();
		}
		Destroy(goHealthInstance);
		Debug.Log(sUnitName + " IS DEAD");
		Destroy(gameObject, aAnimator.GetCurrentAnimatorStateInfo(0).length-0.3f);
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



	public BaseGameStructure GetCurrentBase() {
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


	public void WanderBetweenBasePoints() {
		if(vNavTarget == Vector3.zero || VectorApproximatelyEquals(gameObject.transform.position, vNavTarget, 1.0f) || Vector3.Distance(gameObject.transform.position, vNavTarget) >= 50 || navAgent.pathStatus == NavMeshPathStatus.PathPartial) {
			vNavTarget = goTargetBase.l_tWanderPoints[Random.Range(0,goTargetBase.l_tWanderPoints.Count)].position;
		}
		if(navAgent.destination != vNavTarget) {
			navAgent.SetDestination(vNavTarget);
		}
	}

	//pretty simple what this does, but in all actuality it should be added to some kinda math class
	public bool VectorApproximatelyEquals(Vector3 vec1, Vector3 vec2, float dif){
		if( Mathf.Abs(vec1.x - vec2.x) <= dif && Mathf.Abs(vec1.z - vec2.z) <= dif) {
			return true;
		}
		else {
			return false;
		}
	}


	public bool IsFSMInitialised() {
		if (this.FSM != null) {
			return true;
		}

		return false;
	}
}

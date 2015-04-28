using UnityEngine;
using System.Collections;


public class EnemyUnit : GameUnit {

	private FSM_Core<EnemyUnit> FSM;

	private PlayerResources playerResources;
	[Tooltip("For testing purposes, kills the selected unit")]
	public bool kill;
	public float fWorth;

	public ENEMY_TYPE SollyType = ENEMY_TYPE.NONE;

	private float fTimer = 1.75f;

	// Use this for initialization
	protected override void Start () 
	{
		base.Start();

		playerResources = FindObjectOfType<PlayerResources>();

		this.goTargetBase = GetClosestBase();

		FSM = new FSM_Core<EnemyUnit>();
		FSM.Config(this, new StateEnemyMoveToBase());

	}

	// Update is called once per frame
	protected override void Update () 
	{
		base.Update();
		if (FSM != null) {
			FSM.Update();
		}

		if(kill)
		{
			fHealthCurrent = 0f;
		}

//		if(Vector3.Distance(this.transform.position, goTargetBase.transform.position) <= fRange)
//		{
//			navAgent.SetDestination(this.transform.position);
//		}

	}

	protected override void UnitStopFlashing() {
		if(SollyType != ENEMY_TYPE.TANK){
			SkinnedMeshRenderer UnitsMesh = gameObject.GetComponentInChildren<SkinnedMeshRenderer> ();
			UnitsMesh.enabled = true;
		}
	}
	
	protected override void UnitFlashing() {
		if(SollyType !=ENEMY_TYPE.TANK) {
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
	}

	protected override void KillUnit()
	{
		playerResources.ChangeMoney(fWorth);
		if(wMannedWindow != null){
			wMannedWindow.RemoveTarget();
		}
		this.goTargetBase.l_euAttackers.Remove (this);
		Destroy(this.gameObject);
	}

	
	public void ChangeState(FSM_State<EnemyUnit> gu) {
		FSM.ChangeState(gu);
	}

	public void ReturnToLastState()
	{
		FSM.ReturnToLastState();
	}

	BaseGameStructure GetClosestBase()
	{
		BaseGameStructure returnStructure = StaticGameStructures.bases[0];
				
		float tempdistance = Vector3.Distance(this.transform.position, StaticGameStructures.bases[0].transform.position);

		for(int i = 0; i < StaticGameStructures.bases.Count; i++)
		{
			if( Vector3.Distance(this.transform.position, StaticGameStructures.bases[i].transform.position) < tempdistance)
			{
				tempdistance = Vector3.Distance(this.transform.position, StaticGameStructures.bases[i].transform.position);
				returnStructure = StaticGameStructures.bases[i];
			}

		}

		return returnStructure;
	}
}

using UnityEngine;
using System.Collections;


public class EnemyUnit : GameUnit {

	private FSM_Core<EnemyUnit> FSM;

	private PlayerResources playerResources;

	public float fWorth;

	// Use this for initialization
	protected override void Start () 
	{
		base.Start();

		playerResources = FindObjectOfType<PlayerResources>();

		FSM = new FSM_Core<EnemyUnit>();
		FSM.Config(this, new StateEnemyMoveToBase());

		this.goTargetBase = GetClosestBase();

	}

	// Update is called once per frame
	protected override void Update () 
	{
		base.Update();
		FSM.Update();

//		if(Vector3.Distance(this.transform.position, goTargetBase.transform.position) <= fRange)
//		{
//			navAgent.SetDestination(this.transform.position);
//		}

	}

	protected override void KillUnit()
	{
		playerResources.ChangeMoney(fWorth);
	}

	
	public void ChangeState(FSM_State<EnemyUnit> gu) {
		FSM.ChangeState(gu);
	}

	BaseStructure GetClosestBase()
	{
		BaseStructure returnStructure = staticStructures.bases[0];
				
		float tempdistance = Vector3.Distance(this.transform.position, staticStructures.bases[0].transform.position);

		for(int i = 0; i < staticStructures.bases.Count; i++)
		{
			if( Vector3.Distance(this.transform.position, staticStructures.bases[i].transform.position) < tempdistance)
			{
				tempdistance = Vector3.Distance(this.transform.position, staticStructures.bases[i].transform.position);
				returnStructure = staticStructures.bases[i];
			}

		}

		return returnStructure;
	}
}

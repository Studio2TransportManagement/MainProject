using UnityEngine;
using System.Collections;


public class EnemyUnit : GameUnit {


	// Use this for initialization
	protected override void Start () 
	{
		base.Start();

		this.goTargetBase = GetClosestBase();

		navAgent.SetDestination(goTargetBase.transform.position);
	}

	// Update is called once per frame
	protected override void Update () 
	{
		base.Update();

		if(Vector3.Distance(this.transform.position, goTargetBase.transform.position) <= fRange)
		{
			navAgent.SetDestination(this.transform.position);
		}

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

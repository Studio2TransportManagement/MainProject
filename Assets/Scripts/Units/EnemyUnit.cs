using UnityEngine;
using System.Collections;


public class EnemyUnit : GameUnit {

	NavMeshAgent agent;

	// Use this for initialization
	void Start () 
	{
		agent = GetComponent<NavMeshAgent>();

//		agent.SetDestination(new Vector3(0,0,0));

		this.goTargetBase = GetClosestBase();

		agent.SetDestination(goTargetBase.transform.position);
	}

	// Update is called once per frame
	void Update () 
	{
		if(Vector3.Distance(this.transform.position, goTargetBase.transform.position) <= fRange)
		{
			agent.SetDestination(this.transform.position);
		}
	}

	baseStructure GetClosestBase()
	{
		baseStructure returnStructure = staticStructures.bases[0];
				
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

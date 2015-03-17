using UnityEngine;
using System.Collections;

public class uEnemy_Soldier : GameUnit {

	NavMeshAgent agent;

	GameObject goTargetBase;

	StructureManager structureManager;


	// Use this for initialization
	void Start () 
	{
		agent = GetComponent<NavMeshAgent>();

		agent.SetDestination(new Vector3(0,0,0));

		//goTargetBase = GetClosestBase();

		//agent.SetDestination(goTargetBase.transform.position);

		//Debug.Log(StructureManager.bases[1].transform.position.ToString());
	}

	// Update is called once per frame
	void Update () 
	{
//		if(Vector3.Distance(this.transform.position, goTargetBase.transform.position) <= fRange)
//		{
//			agent.SetDestination(this.transform.position);
//		}
	}

	GameObject GetClosestBase()
	{
		GameObject returnObject = StructureManager.bases[0];
				
		float tempdistance = (float)10000.0;

		for(int i = 0; i < StructureManager.bases.Length; i++)
		{
			if( Vector3.Distance(this.transform.position, StructureManager.bases[i].transform.position) < tempdistance)
			{
				tempdistance = Vector3.Distance(this.transform.position, StructureManager.bases[i].transform.position);
				returnObject = StructureManager.bases[i];
			}

		}

		return returnObject;
	}
}

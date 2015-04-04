using UnityEngine;
using System.Collections;


public class EnemyUnit : GameUnit {

	NavMeshAgent agent;

	GameObject goTargetBase;

	// Use this for initialization
	void Start () 
	{
		agent = GetComponent<NavMeshAgent>();

//		agent.SetDestination(new Vector3(0,0,0));

		goTargetBase = GetClosestBase();

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

	GameObject GetClosestBase()
	{
		GameObject returnObject = staticStructures.bases[0].gameObject;
				
		float tempdistance = Vector3.Distance(this.transform.position, staticStructures.bases[0].gameObject.transform.position);

		for(int i = 0; i < staticStructures.bases.Count; i++)
		{
			if( Vector3.Distance(this.transform.position, staticStructures.bases[i].gameObject.transform.position) < tempdistance)
			{
				tempdistance = Vector3.Distance(this.transform.position, staticStructures.bases[i].gameObject.transform.position);
				returnObject = staticStructures.bases[i].gameObject;
			}

		}

		return returnObject;
	}
}

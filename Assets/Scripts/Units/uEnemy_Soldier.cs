using UnityEngine;
using System.Collections;

public class uEnemy_Soldier : GameUnit {

	NavMeshAgent agent;

	StructureManager structureManager;

	// Use this for initialization
	void Start () 
	{
		agent = GetComponent<NavMeshAgent>();
		agent.SetDestination(new Vector3(0,2,0));

//		Debug.Log(StructureManager.bases[1].transform.position.ToString());
	}

	// Update is called once per frame
	void Update () {

	}

	Vector3 GetClosestBase()
	{
		Vector3 returnVector = new Vector3(0,0,0);


//
//		for(int i = 0; i < ; i++)
//		{
//			Debug.Log();
////		}

		return returnVector;
	}
}

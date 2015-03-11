using UnityEngine;
using System.Collections;

public class uEnemy_Soldier : GameUnit {

	NavMeshAgent agent;



	// Use this for initialization
	void Start () 
	{
		agent = GetComponent<NavMeshAgent>();
		agent.SetDestination(new Vector3(0,2,0));
	}

	// Update is called once per frame
	void Update () {

	}
}

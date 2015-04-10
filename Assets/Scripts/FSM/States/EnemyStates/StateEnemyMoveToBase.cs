using UnityEngine;
using System.Collections;

public class StateEnemyMoveToBase : FSM_State<EnemyUnit>{

	public StateEnemyMoveToBase() {
		Debug.Log("StateEnemyMoveToBase Constructed");
	}
	
	public override void Begin(EnemyUnit eu) {
				
		eu.navAgent.SetDestination(eu.goTargetBase.transform.position);
		Debug.Log ("StateEnemyMoveToBase has begun");
	}
	
	public override void Run(EnemyUnit eu) {

		if(Vector3.Distance(eu.transform.position, eu.goTargetBase.transform.position) <= eu.fRange)
		{
			eu.navAgent.SetDestination(eu.transform.position);
		}
				
	}
	
	public override void End(EnemyUnit gu) {
		Debug.Log("StateSoldierIdle end");
	}
}

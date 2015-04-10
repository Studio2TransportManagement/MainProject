using UnityEngine;
using System.Collections;

public sealed class StateEnemyMoveToBase : FSM_State<EnemyUnit>{

	public StateEnemyMoveToBase() {

	}
	
	public override void Begin(EnemyUnit eu) {
		Debug.Log ("MoveToBase Begin");		
		eu.navAgent.SetDestination(eu.goTargetBase.transform.position);
		Debug.Log ("StateEnemyMoveToBase has begun");
	}
	
	public override void Run(EnemyUnit eu) {

		if(Vector3.Distance(eu.transform.position, eu.goTargetBase.transform.position) <= eu.fRange)
		{
			eu.navAgent.SetDestination(eu.transform.position);

			eu.ChangeState(new StateEnemyFiring());

		}
				
	}
	
	public override void End(EnemyUnit gu) {
		Debug.Log("StateSoldierIdle end");
	}
}

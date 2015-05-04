using UnityEngine;
using System.Collections;

public sealed class StateEnemyMoveToBase : FSM_State<EnemyUnit>{

	public StateEnemyMoveToBase() {

	}
	
	public override void Begin(EnemyUnit eu) {
		eu.navAgent.SetDestination(eu.goTargetBase.transform.position);
//		Debug.Log ("StateEnemyMoveToBase has begun");
	}
	
	public override void Run(EnemyUnit eu) {

		if(eu.AtBase)
		{
			eu.ChangeState(new StateEnemyFiring());
		}
				
	}
	
	public override void End(EnemyUnit eu) {
		eu.navAgent.Stop();
		eu.navAgent.SetDestination(eu.v3BasePos);
//		Debug.Log("StateSoldierIdle end");
	}
}

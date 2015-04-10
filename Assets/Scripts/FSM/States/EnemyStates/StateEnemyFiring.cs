using UnityEngine;
using System.Collections;

public sealed class StateEnemyFiring : FSM_State<EnemyUnit> {

	public StateEnemyFiring() {

	}
	
	public override void Begin(EnemyUnit eu) 
	{
		Debug.Log ("StateEnemyFiring Begin");	
	}
	
	public override void Run(EnemyUnit eu) {
		
			
	}
	
	public override void End(EnemyUnit gu) {

	}
}

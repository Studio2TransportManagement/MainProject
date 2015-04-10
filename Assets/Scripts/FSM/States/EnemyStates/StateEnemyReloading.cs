using UnityEngine;
using System.Collections;

public sealed class StateEnemyReloading : FSM_State<EnemyUnit> {

	private float fReloadTimer;

	public StateEnemyReloading() {
		
	}
	
	public override void Begin(EnemyUnit eu) {
//		Debug.Log ("Reloading Begun");
		fReloadTimer = eu.fReloadSpeed;
	}
	
	public override void Run(EnemyUnit eu) {
		
		if(fReloadTimer > 0)
		{
			fReloadTimer -= Time.deltaTime;
		}
		if(fReloadTimer <=0)
		{
			eu.iCurrentAmmo = eu.iMaxAmmo;
			Debug.Log ("<color=cyan>Reloaded</color>");
			eu.ReturnToLastState();
		}
		
	}
	
	public override void End(EnemyUnit eu) {
//		Debug.Log("StateSoldierReload end");
	}
}

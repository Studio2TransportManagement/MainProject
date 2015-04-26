using UnityEngine;
using System.Collections;

public sealed class StateEnemyFiring : FSM_State<EnemyUnit> {

	private float tempShoot;

	public StateEnemyFiring() {

	}
	
	public override void Begin(EnemyUnit eu) 
	{
//		Debug.Log ("StateEnemyFiring Begin");

	}
	
	public override void Run(EnemyUnit eu) {
		if(eu.iCurrentAmmo > 0){
			if(tempShoot != 0f)
			{
				tempShoot -= Time.deltaTime;
			}

			if(tempShoot <= 0f)
			{
				if(eu.goTargetBase.CheckForTargetableWindow()) {
					eu.wMannedWindow = eu.goTargetBase.TargetAvailableOpenWindow();
					eu.guTargetUnit = eu.wMannedWindow.TargetWindow(eu);
					eu.guTargetUnit.DamageUnit(eu.fDamage);
					//Debug.Log ("<color=green>Firing at Window Unit</color>");
				}
				else {
					eu.goTargetBase.ModifyCurrentIntegrity(-eu.fDamage);
					//Debug.Log ("<color=green>Firing at Base</color>");
				}
				eu.iCurrentAmmo--;
				tempShoot = eu.fFireRate;
			}
		}
		if(eu.iCurrentAmmo <= 0)
		{
			eu.ChangeState(new StateEnemyReloading());
		}
	}
	
	public override void End(EnemyUnit gu) {

	}
}

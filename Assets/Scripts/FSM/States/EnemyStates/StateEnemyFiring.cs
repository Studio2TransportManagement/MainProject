using UnityEngine;
using System.Collections;

public sealed class StateEnemyFiring : FSM_State<EnemyUnit> {

	private float tempShoot;

	public StateEnemyFiring() {

	}
	
	public override void Begin(EnemyUnit eu) {
		eu.aAnimator.SetBool("bIsAiming", true);
		eu.goFiringEffect.gameObject.SetActive(true);
		eu.goFiringEffect.Play("Bullet Effect");
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
					eu.guTargetUnit.DamageUnit(eu.fDamage, eu);
					eu.transform.LookAt(new Vector3(eu.guTargetUnit.transform.position.x, eu.transform.position.y, eu.guTargetUnit.transform.position.z));
					//Debug.Log ("<color=green>Firing at Window Unit</color>");
				}
				else {
					eu.goTargetBase.DamageBase(eu.fDamage, eu);
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
		gu.aAnimator.SetBool("bIsAiming", false);
		gu.goFiringEffect.gameObject.SetActive(false);
	}
}

﻿using UnityEngine;
using System.Collections;

public sealed class StateEnemyFiring : FSM_State<EnemyUnit> {

	private float tempShoot;

	public StateEnemyFiring() {

	}
	
	public override void Begin(EnemyUnit eu) {
		eu.aAnimator.SetBool("bIsAiming", true);
		eu.goFiringEffect.gameObject.SetActive(true);
		if(eu.SollyType == SOLDIER_TYPE.ENEMY_GUNNER) {
			eu.goFiringEffect.Play("Bullet Effect");
			eu.asAudioSource.clip = eu.uaUnitAudio.acFiring;
			eu.asAudioSource.loop = true;
			eu.asAudioSource.volume = 0.2f;
			eu.asAudioSource.Play();
		}
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
					if (eu.SollyType == SOLDIER_TYPE.ENEMY_TANK) {
						GameObject.Instantiate(eu.goParticleActionEffectPrefab,
						                       eu.transform.position + new Vector3(0.0f, 0.5f, 0.0f) + (eu.transform.forward * 2),
						                       Quaternion.identity
                        );
					}
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

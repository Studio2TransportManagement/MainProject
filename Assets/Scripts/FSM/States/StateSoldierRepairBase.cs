﻿using UnityEngine;
using System.Collections;

public class StateSoldierRepairBase : FSM_State<PlayerUnit> {

	private float fRepairTimer;
	
	public StateSoldierRepairBase() {
		//
	}
	
	public override void Begin(PlayerUnit gu) {
		gu.navAgent.SetDestination(gu.goTargetBase.RepairLocation.position);
		gu.asAudioSource.clip = gu.uaUnitAudio.acReloading;
		gu.asAudioSource.loop = true;
		gu.asAudioSource.volume = 1.0f;
		//Debug.Log("StateSoldierRepairBase begin");
	}
	
	public override void Run(PlayerUnit gu) {
		//Stop if we don't need repairs
		if (gu.goTargetBase.fHealthCurrent <= 0 || gu.goTargetBase.fHealthCurrent >= gu.goTargetBase.fHealthMax) {
			gu.ChangeState(new StateSoldierIdle());
		}
		else if (gu.goTargetBase != null && gu.navAgent.remainingDistance <= 2.0f) {
			gu.navAgent.Stop();
			gu.navAgent.SetDestination(gu.transform.position);
			gu.asAudioSource.Play();
			
			gu.aAnimator.SetBool("bIsRummaging", true);
			if(gu.iCurrentAmmo > 0) {
				if (fRepairTimer != 0) {
					fRepairTimer -= Time.fixedDeltaTime;

				}
				
				if (fRepairTimer <= 0) {
					gu.goTargetBase.Repair(gu.fDamage);
					gu.iCurrentAmmo--;
					fRepairTimer = gu.fFireRate;

					if (gu.goParticleActionEffectPrefab != null) {
						GameObject.Instantiate(gu.goParticleActionEffectPrefab, gu.transform.position + new Vector3(0.0f, 0.5f, 0.0f), Quaternion.identity);
					}
				}
			}
			if(gu.iCurrentAmmo <= 0) {
				gu.ChangeState(new StateSoldierReload());
			}
		}		
	}
	
	public override void End(PlayerUnit gu) {
		gu.asAudioSource.Stop();
		gu.asAudioSource.loop = false;
		gu.aAnimator.SetBool("bIsRummaging", false);

		Debug.Log("StateSoldierRepairBase end");
	}
}

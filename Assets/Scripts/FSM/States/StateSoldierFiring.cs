using UnityEngine;
using System.Collections;

public sealed class StateSoldierFiring : FSM_State<PlayerUnit> {

	private float fShootTimer;

	public StateSoldierFiring() {
		//
	}
	
	public override void Begin(PlayerUnit gu) {
		gu.aAnimator.SetBool("bIsAiming", true);
		gu.goFiringEffect.gameObject.SetActive(true);
		gu.goFiringEffect.Play("Bullet Effect");
		//Debug.Log("StateSoldierFiring begin");
	}
	
	public override void Run(PlayerUnit gu) {
		if (gu.goTargetBase.l_euAttackers.Count > 0) {
				gu.guTargetUnit = gu.goTargetBase.l_euAttackers[0];
		} 
		else {
			//No attackers
			gu.ChangeState(new StateSoldierAlert());
		}

		if (gu.guTargetUnit != null) {
			gu.transform.LookAt(new Vector3(gu.guTargetUnit.transform.position.x, gu.transform.position.y, gu.guTargetUnit.transform.position.z));
			if (gu.iCurrentAmmo > 0) {
				if(fShootTimer > 0) {
					fShootTimer -= Time.fixedDeltaTime;
				}
				
				if (fShootTimer <= 0f) {
					gu.guTargetUnit.DamageUnit(gu.fDamage, gu);
					gu.iCurrentAmmo--;
					fShootTimer = gu.fFireRate;
				}
			}
		}

		if (gu.iCurrentAmmo <= 0) {
			gu.ChangeState(new StateSoldierReload());
		}
	}
	
	public override void End(PlayerUnit gu) {
		gu.aAnimator.SetBool("bIsAiming", false);
		gu.goFiringEffect.gameObject.SetActive(false);
		//Debug.Log("StateSoldierFiring end");
	}
}

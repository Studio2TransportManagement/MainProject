using UnityEngine;
using System.Collections;

public sealed class StateSoldierFiring : FSM_State<PlayerUnit> {

	private float fShootTimer;

	public StateSoldierFiring() {
		//
	}
	
	public override void Begin(PlayerUnit gu) {
		gu.aAnimator.SetBool("bIsAiming", true);
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
			if (gu.iCurrentAmmo > 0) {
				if(fShootTimer != 0f) {
					fShootTimer -= Time.fixedDeltaTime;
				}
				
				if (fShootTimer <= 0f) {
					gu.guTargetUnit.DamageUnit(gu.fDamage);
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

		//Debug.Log("StateSoldierFiring end");
	}
}

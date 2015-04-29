using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateSoldierHealAlly : FSM_State<PlayerUnit> {

	private float fHealTimer;
	
	public StateSoldierHealAlly() {
		//
	}
	
	public override void Begin(PlayerUnit gu) {
		
		//Debug.Log("StateSoldierHealAlly begin");
	}
	
	public override void Run(PlayerUnit gu) {
		//Don't heal ourselves
		if (gu.guTargetUnit == gu) {
			gu.guTargetUnit = null;
		}

		//If they're fully healed or have vanished, don't try and heal them
		if (gu.guTargetUnit != null && gu.guTargetUnit.fHealthCurrent < gu.guTargetUnit.fHealthMax) {
			if (gu.iCurrentAmmo > 0) {
				if (fHealTimer != 0f) {
					fHealTimer -= Time.fixedDeltaTime;
				}
				
				if (fHealTimer <= 0) {
					if (gu.guTargetUnit != null) {
						gu.guTargetUnit.HealUnit(gu.fDamage);
						gu.iCurrentAmmo--;
					}
					fHealTimer = gu.fFireRate;
				}
			}
		} 
		else {
			//No heal target
			gu.ChangeState(new StateSoldierIdle());
		}

		if (gu.iCurrentAmmo <= 0) {
			gu.ChangeState(new StateSoldierReload());
		}
	}
	
	public override void End(PlayerUnit gu) {
		//Debug.Log("StateSoldierHealAlly end");
	}
}

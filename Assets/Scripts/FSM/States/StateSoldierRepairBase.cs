using UnityEngine;
using System.Collections;

public class StateSoldierRepairBase : FSM_State<PlayerUnit> {

	private float fRepairTimer;
	
	public StateSoldierRepairBase() {
		//
	}
	
	public override void Begin(PlayerUnit gu) {
		//Debug.Log("StateSoldierRepairBase begin");
	}
	
	public override void Run(PlayerUnit gu) {
		//Stop if we don't need repairs
		if (gu.goTargetBase.fHealthCurrent <= 0 || gu.goTargetBase.fHealthCurrent >= gu.goTargetBase.fHealthMax) {
			gu.ChangeState(new StateSoldierIdle());
		}
		else if (gu.iCurrentAmmo > 0) {
			if (fRepairTimer != 0) {
				fRepairTimer -= Time.fixedDeltaTime;
			}
			
			if (fRepairTimer <= 0) {
				gu.goTargetBase.Repair(gu.fDamage);
				gu.iCurrentAmmo--;
				fRepairTimer = gu.fFireRate;
			}
		}
		
		if(gu.iCurrentAmmo <= 0) {
			gu.ChangeState(new StateSoldierReload());
		}
		
	}
	
	public override void End(PlayerUnit gu) {
		//Debug.Log("StateSoldierRepairBase end");
	}
}

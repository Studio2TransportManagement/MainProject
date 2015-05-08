using UnityEngine;

public sealed class StateSoldierIdle : FSM_State<PlayerUnit> {

	public StateSoldierIdle() {
		//Debug.Log("StateSoldierIdle begin");
	}
	
	public override void Begin(PlayerUnit gu) {
		gu.navAgent.speed = gu.fIdleSpeed;
		gu.navAgent.angularSpeed = 180.0f;
		gu.goTargetBase = gu.GetCurrentBase();
		gu.GetCurrentBase();
		if(gu.bManningWindow) {
			gu.wMannedWindow.LeaveWindow();
		}
		gu.WanderBetweenBasePoints();

	}
	
	public override void Run(PlayerUnit gu) {
		if (gu.goTargetBase != null) {
			//Gunnner + Heavy
			if (gu.SollyType == SOLDIER_TYPE.GUNNER || gu.SollyType == SOLDIER_TYPE.HEAVY || gu.SollyType == SOLDIER_TYPE.VILLAGER) {
				if (gu.goTargetBase.bAlert) {
					gu.ChangeState(new StateSoldierAlert());
				}
				else {
					gu.WanderBetweenBasePoints();
				}
			}
			//Medic
			else if (gu.SollyType == SOLDIER_TYPE.MEDIC) {
				if (gu.goTargetBase == null) {
					Debug.LogError("MEDIC DIDNT GET A BASE");
				}

				if (gu.goTargetBase.GetInjuredUnitsInBase().Count > 0) {
					gu.guTargetUnit = gu.goTargetBase.GetInjuredUnitsInBase()[0];

					foreach (GameUnit curunit in gu.goTargetBase.GetInjuredUnitsInBase()) {
						if (curunit.fHealthCurrent > 0 && curunit.fHealthCurrent < gu.guTargetUnit.fHealthCurrent) {
							gu.guTargetUnit = curunit;
						}
					}

					if (gu.guTargetUnit == gu && gu.goTargetBase.GetInjuredUnitsInBase().Count > 1) {
						gu.guTargetUnit = gu.goTargetBase.GetInjuredUnitsInBase()[1];
					}

					gu.navAgent.SetDestination(gu.guTargetUnit.transform.position);
					if (gu.guTargetUnit != null && gu.navAgent.remainingDistance <= 1.5f ) {
						gu.ChangeState(new StateSoldierHealAlly());
					}
				}
				else {
					gu.WanderBetweenBasePoints();
				}
			}
			//Mechanic
			else if (gu.SollyType == SOLDIER_TYPE.MECHANIC) {
				if (gu.goTargetBase.fHealthCurrent < gu.goTargetBase.fHealthMax) {
						gu.ChangeState(new StateSoldierRepairBase());
				}
				else {
					gu.WanderBetweenBasePoints();
				}
			}

		}

		else {
			Debug.Log("StateSoldierIdle (" + gu.SollyType.ToString() + "): goTargetBase was null!");
		}
		
	}
	
	public override void End(PlayerUnit gu) {
		//Debug.Log("StateSoldierIdle end");
	}
}

using UnityEngine;

public sealed class StateSoldierIdle : FSM_State<PlayerUnit> {

	private Vector3 vTarget;
	private Vector3 vDir;
//	private float fWanderRate = 0.5f;
//	private float fWanderDistance = 20.0f;
	private NavMeshHit navhitIrrelevant;


	public StateSoldierIdle() {
		//Debug.Log("StateSoldierIdle begin");
	}

	public StateSoldierIdle(float wanderRate, float wanderDistance) {
//		fWanderRate = wanderRate;
//		fWanderDistance = wanderDistance;
	}
	
	public override void Begin(PlayerUnit gu) {
		gu.navAgent.speed = 1.0f;
		gu.navAgent.angularSpeed = 180.0f;
		gu.goTargetBase = gu.GetCurrentBase();
		gu.aAnimator.SetBool("bIsWalking", true);
//		navhitIrrelevant = new NavMeshHit();
	}
	
	public override void Run(PlayerUnit gu) {
//		do {
//			vDir = gu.transform.forward + new Vector3(Random.insideUnitCircle.x, gu.transform.position.y, Random.insideUnitCircle.y) * fWanderRate;
//			vTarget = gu.transform.position + vDir.normalized * fWanderDistance;
//		}
//		while (NavMesh.SamplePosition(vTarget,
//		                              out navhitIrrelevant,
//		                              Mathf.Infinity,
//		                              NavMesh.GetNavMeshLayerFromName("Floorges")));
//
//		gu.navAgent.SetDestination(vTarget);

		if (gu.goTargetBase != null) {
			//Gunnner + Heavy
			if (gu.SollyType == SOLDIER_TYPE.GUNNER || gu.SollyType == SOLDIER_TYPE.HEAVY) {
				if (gu.goTargetBase.bAlert) {
					gu.ChangeState(new StateSoldierAlert());
				}
			}
			//Medic
			else if (gu.SollyType == SOLDIER_TYPE.MEDIC) {
				if (gu.goTargetBase.GetInjuredUnitsInBase().Count > 0) {
					gu.guTargetUnit = gu.goTargetBase.GetInjuredUnitsInBase()[0];

					foreach (GameUnit curunit in gu.goTargetBase.GetInjuredUnitsInBase()) {
						if (curunit.fHealthCurrent > 0 && curunit.fHealthCurrent < gu.guTargetUnit.fHealthCurrent) {
							gu.guTargetUnit = curunit;
						}
					}

					gu.ChangeState(new StateSoldierHealAlly());
				}
			}
			//Mechanic
			else if (gu.SollyType == SOLDIER_TYPE.MECHANIC) {
				if (gu.goTargetBase.fHealthCurrent < gu.goTargetBase.fHealthMax) {
					gu.ChangeState(new StateSoldierRepairBase());
				}
			}
			gu.WanderBetweenBasePoints();
		}

		else {
			Debug.Log("StateSoldierIdle (" + gu.SollyType.ToString() + "): goTargetBase was null!");
		}
		
	}
	
	public override void End(PlayerUnit gu) {
		gu.aAnimator.SetBool("bIsWalking", false);

		gu.navAgent.speed = 3.0f;
		//Debug.Log("StateSoldierIdle end");
	}
}

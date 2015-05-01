using UnityEngine;
using System.Collections;

public class StateSoldierAlert : FSM_State<PlayerUnit> {

	private bool bGoForWindow = false;
	
	public StateSoldierAlert() {
		//Debug.Log("StateSoldierAlert begin");
	}

	public override void Begin(PlayerUnit gu) {
		gu.aAnimator.SetBool("bIsRunning", true);
		gu.navAgent.speed = gu.fAlertSpeed;
		//Debug.Log("StateSoldierAlert begin");
	}
	
	public override void Run(PlayerUnit gu) {
		if (gu.goTargetBase != null) {
			if (!gu.bManningWindow) {
				if (gu.SollyType == SOLDIER_TYPE.HEAVY) {
					bGoForWindow = false;

					foreach (EnemyUnit eu in gu.goTargetBase.l_euAttackers) {
						if (eu.SollyType == SOLDIER_TYPE.ENEMY_TANK) {
							bGoForWindow = true;
							break;
						}
					}
				}
				else {
					bGoForWindow = true;
				}

				if (bGoForWindow) {
					if (gu.goTargetBase.CheckIfWindowAvailable()) {
						gu.goTargetBase.GetAvailableOpenWindow().ManWindow(gu);
						gu.navAgent.SetDestination(gu.wMannedWindow.tStandingPosition.position);
						//Debug.Log ("Manning Window");
					}
					else {
						if (gu.SollyType == SOLDIER_TYPE.HEAVY) {
							foreach (Window win in gu.goTargetBase.GetMannedWindows()) {
								if (win.goStationedSoldier.SollyType == SOLDIER_TYPE.GUNNER) {
									win.LeaveWindow();
									win.ManWindow(gu);
									break;
								}
							}
						}
						
						if (gu.SollyType == SOLDIER_TYPE.VILLAGER) {
							foreach (Window win in gu.goTargetBase.GetMannedWindows()) {
								if (win.goStationedSoldier.SollyType != SOLDIER_TYPE.VILLAGER) {
									win.LeaveWindow();
									win.ManWindow(gu);
									break;
								}
							}
						}
						//Debug.Log("<color=red>No AvailableWindows</color>");
					}
				}

				if (!gu.goTargetBase.bAlert) {
					gu.ChangeState(new StateSoldierIdle());
				}

				gu.WanderBetweenBasePoints();
			}
			else {
				//only start firing if theres enemies at the base and we're close enough to the window to actually start attacking
				if (gu.goTargetBase.l_euAttackers.Count > 0 && gu.navAgent.remainingDistance <= 0.3f ) {
					gu.ChangeState(new StateSoldierFiring());
				}

				if(gu.goTargetBase.l_euAttackers.Count == 0) {
					gu.ChangeState(new StateSoldierIdle());
				}

			}
		}
		else {
			Debug.Log("StateSoldierAlert(" + gu.SollyType.ToString() + "): gu.goTargetBase is null!");
		}
	}
	
	public override void End(PlayerUnit gu) {
		gu.aAnimator.SetBool("bIsRunning", false);

		if (gu.bManningWindow && gu.goTargetBase.l_euAttackers.Count == 0) {
			gu.wMannedWindow.LeaveWindow();
//			Debug.Log("StateSoldierAlert leftwin end");
		}
		//Debug.Log("StateSoldierAlert end");
	}
}

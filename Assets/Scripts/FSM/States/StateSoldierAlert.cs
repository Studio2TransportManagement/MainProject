using UnityEngine;
using System.Collections;

public class StateSoldierAlert : FSM_State<PlayerUnit> {

	
	public StateSoldierAlert() {
		Debug.Log("StateSoldierAlert begin");
	}
		
	public override void Begin(PlayerUnit gu) {


	}
	
	public override void Run(PlayerUnit gu) {
		if (gu.goTargetBase != null) {
			if(!gu.bManningWindow) {
				if (gu.goTargetBase != null) {
					if(gu.goTargetBase.CheckIfWindowAvailable()) {
						gu.wMannedWindow = gu.goTargetBase.GetAvailableOpenWindow();
						gu.wMannedWindow.ManWindow(gu);
						gu.bManningWindow = true;
						gu.navAgent.SetDestination(gu.wMannedWindow.tStandingPosition.position);
						//					Debug.Log ("Manning Window");
					}
					else {
						Debug.Log("<color=red>No AvailableWindows</color>");
					}
				}
				else{
					Debug.Log("<color = red>Base is NULL</color>");
				}
			}
			else {
				if(gu.goTargetBase.l_euAttackers.Count > 0) {
					gu.ChangeState(new StateSoldierFiring());
				}
				else{
					gu.ChangeState(new StateSoldierIdle());
				}
			}
		}
		else {
			Debug.Log("StateSoldierAlert: gu.goTargetBase is null!");
		}
	}
	
	public override void End(PlayerUnit gu) {

		Debug.Log("StateSoldierAlert end");
	}
}

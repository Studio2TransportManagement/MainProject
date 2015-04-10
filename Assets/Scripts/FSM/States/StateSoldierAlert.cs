using UnityEngine;
using System.Collections;

public class StateSoldierAlert : FSM_State<PlayerUnit> {

	
	public StateSoldierAlert() {
		Debug.Log("StateSoldierAlert begin");
	}
		
	public override void Begin(PlayerUnit gu) {
		if(!gu.bManningWindow)
		{
			if (gu.goTargetBase != null) {
				if(gu.goTargetBase.CheckIfWindowAvailable()) {
					gu.wMannedWindow = gu.goTargetBase.GetAvailableOpenWindow();
					gu.wMannedWindow.ManWindow(gu);
					gu.bManningWindow = true;
					gu.navAgent.destination = gu.wMannedWindow.tStandingPosition.position;
					Debug.Log ("Manning Window");
				}
				else {
					Debug.Log("No AvailableWindows");
				}
			}
			else{
				Debug.Log("<color = red>NULL</color>");
			}
		}
	}
	
	public override void Run(PlayerUnit gu) {
		if (gu.goTargetBase != null) {

		}
		else {
			Debug.Log("StateSoldierAlert: gu.goTargetBase is null!");
		}
	}
	
	public override void End(PlayerUnit gu) {

		Debug.Log("StateSoldierAlert end");
	}
}

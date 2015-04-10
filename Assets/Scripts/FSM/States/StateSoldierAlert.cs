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
			//
		}
		else {
			Debug.Log("StateSoldierAlert: gu.goTargetBase is null!");
		}
	}
	
	public override void End(PlayerUnit gu) {
		Debug.Log("StateSoldierAlert end");
	}
}

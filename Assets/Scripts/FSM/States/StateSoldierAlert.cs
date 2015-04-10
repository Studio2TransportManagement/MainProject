using UnityEngine;
using System.Collections;

public class StateSoldierAlert : FSM_State<PlayerUnit> {

	
	public StateSoldierAlert() {
		Debug.Log("StateSoldierAlert begin");
	}
		
	public override void Begin(PlayerUnit gu) {
		
	}
	
	public override void Run(PlayerUnit gu) {
				
	}
	
	public override void End(PlayerUnit gu) {
		Debug.Log("StateSoldierAlert end");
	}
}

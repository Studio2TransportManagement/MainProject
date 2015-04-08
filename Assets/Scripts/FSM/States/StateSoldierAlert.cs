using UnityEngine;
using System.Collections;

public class StateSoldierAlert : FSM_State<Gunner> {

	
	public StateSoldierAlert() {
		Debug.Log("StateSoldierAlert begin");
	}
		
	public override void Begin(Gunner gu) {
		
	}
	
	public override void Run(Gunner gu) {
				
	}
	
	public override void End(Gunner gu) {
		Debug.Log("StateSoldierAlert end");
	}
}

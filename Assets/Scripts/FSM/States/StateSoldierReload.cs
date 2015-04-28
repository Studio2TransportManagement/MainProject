using UnityEngine;
using System.Collections;

public sealed class StateSoldierReload : FSM_State<PlayerUnit> {

	private float fReloadTimer;
	
	public StateSoldierReload() {
		
	}
	
	public override void Begin(PlayerUnit gu) {
		//Debug.Log ("StateSoldierReload Begun");
		fReloadTimer = gu.fReloadSpeed;
	}
	
	public override void Run(PlayerUnit gu) {
		
		if(fReloadTimer > 0)
		{
			fReloadTimer -= Time.deltaTime;
		}
		if(fReloadTimer <=0)
		{
			gu.iCurrentAmmo = gu.iMaxAmmo;
			gu.ChangeState(new StateSoldierAlert());
		}
		
	}
	
	public override void End(PlayerUnit gu) {
		//Debug.Log("StateSoldierReload end");
	}
}

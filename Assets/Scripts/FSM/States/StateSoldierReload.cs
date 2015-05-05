using UnityEngine;
using System.Collections;

public sealed class StateSoldierReload : FSM_State<PlayerUnit> {

	private float fReloadTimer;
	private bool playedAudio = false;
	
	public StateSoldierReload() {
		
	}
	
	public override void Begin(PlayerUnit gu) {
		//Debug.Log ("StateSoldierReload Begun");
		fReloadTimer = gu.fReloadSpeed;
		gu.navAgent.SetDestination(gu.goTargetBase.goStockpile.transform.position);
		if(gu.bManningWindow) {
			gu.wMannedWindow.LeaveWindow();
		}
	}
	
	public override void Run(PlayerUnit gu) {
		
		if(Vector3.Distance(gu.gameObject.transform.position, gu.goTargetBase.goStockpile.transform.position) <=4.0f) {
			gu.navAgent.Stop();
			gu.navAgent.SetDestination(gu.gameObject.transform.position);
			gu.aAnimator.SetBool("bIsRummaging", true);
			if(!playedAudio) {
				AudioSource.PlayClipAtPoint(gu.uaUnitAudio.acReloading, gu.gameObject.transform.position, 0.1f);
				playedAudio = true;
			}
			if(fReloadTimer > 0)
			{
				fReloadTimer -= Time.deltaTime;
			}
			if(fReloadTimer <=0)
			{
				gu.iCurrentAmmo = gu.iMaxAmmo;
				if(gu.SollyType == SOLDIER_TYPE.GUNNER || gu.SollyType == SOLDIER_TYPE.HEAVY) {
					gu.ChangeState(new StateSoldierAlert());
				}
				else {
					gu.ChangeState(new StateSoldierIdle());
				}
			}
		}
		
	}
	
	public override void End(PlayerUnit gu) {
		gu.aAnimator.SetBool("bIsRummaging", false);
		//Debug.Log("StateSoldierReload end");
	}
}

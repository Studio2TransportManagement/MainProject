using UnityEngine;

public sealed class StateSoldierMoveToBase : FSM_State<PlayerUnit> {
	
	public BaseStructure bsBaseToReach {
		get;
		set;
	}

	public GameObject goTargetStation;
	
	
	public StateSoldierMoveToBase() {
		bsBaseToReach = null;
		goTargetStation = null;
	}
	
	public StateSoldierMoveToBase(BaseStructure goalbase) {
		bsBaseToReach = goalbase;
	}
	
	public override void Begin(PlayerUnit gu) {
		Debug.Log("StateSoldierMoveToBase begin");
		gu.bInTransit = true;
	}
	
	public override void Run(PlayerUnit gu) {
		if (bsBaseToReach != null) {
			if (gu.goTargetBase != bsBaseToReach) {
				FindClosestTrainStation(gu);
			}
			else {
				//Success! We got there!
				gu.ChangeState(new StateSoldierIdle());
			}
		}
		else {
			Debug.Log("<color=red>StateSoldierMoveToBase: bsBaseToReach was null!</color>");
		}
	}
	
	public override void End(PlayerUnit gu) {
		Debug.Log("StateSoldierMoveToBase end");
		gu.bInTransit = false;
	}

<<<<<<< HEAD
//	public GameObject FindClosestTrainStation(PlayerUnit gu) {
//		if (gu.goTargetBase.sBaseName == "Base Alpha") {
//			//
//		}
//	}
=======
	public void FindClosestTrainStation(PlayerUnit gu) {
		//Alpha to others
		if (gu.goTargetBase.sBaseName == "Base Alpha") {
			if (bsBaseToReach.sBaseName == "Base Bravo") {
				goTargetStation = gu.goTargetBase.goRightStation;
			}
			if (bsBaseToReach.sBaseName == "Base Charlie") {
				goTargetStation = gu.goTargetBase.goLeftStation;
			}
		}

		//Bravo to others
		if (gu.goTargetBase.sBaseName == "Base Bravo") {
			if (bsBaseToReach.sBaseName == "Base Alpha") {
				goTargetStation = gu.goTargetBase.goRightStation;
			}
			if (bsBaseToReach.sBaseName == "Base Charlie") {
				goTargetStation = gu.goTargetBase.goLeftStation;
			}
		}

		//Charlie to others
		if (gu.goTargetBase.sBaseName == "Base Charlie") {
			if (bsBaseToReach.sBaseName == "Base Alpha") {
				goTargetStation = gu.goTargetBase.goLeftStation;
			}
			if (bsBaseToReach.sBaseName == "Base Bravo") {
				goTargetStation = gu.goTargetBase.goRightStation;
			}
		}
	}
>>>>>>> 08cf291ca5f5b4a8301cc5a41a3e3e1e12279167
}

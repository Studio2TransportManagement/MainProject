using UnityEngine;

public sealed class StateSoldierMoveToBase : FSM_State<PlayerUnit> {
	
	public BaseStructure bsBaseToReach {
		get;
		set;
	}

	public TrainStation goTargetStation;
	
	
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
				if(FindClosestTrainStation(gu)!= null)
				gu.navAgent.SetDestination(FindClosestTrainStation(gu).gameObject.transform.position);
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

	public TrainStation FindClosestTrainStation(PlayerUnit gu) {
		//Alpha to others
		if (gu.goTargetBase.sBaseName == "Base Alpha") {
			if (bsBaseToReach.sBaseName == "Base Bravo") {
				return gu.goTargetBase.tsRightStation;
			}
			if (bsBaseToReach.sBaseName == "Base Charlie") {
				return gu.goTargetBase.tsLeftStation;
			}
		}

		//Bravo to others
		if (gu.goTargetBase.sBaseName == "Base Bravo") {
			if (bsBaseToReach.sBaseName == "Base Alpha") {
				return gu.goTargetBase.tsRightStation;
			}
			if (bsBaseToReach.sBaseName == "Base Charlie") {
				return gu.goTargetBase.tsLeftStation;
			}
		}

		//Charlie to others
		if (gu.goTargetBase.sBaseName == "Base Charlie") {
			if (bsBaseToReach.sBaseName == "Base Alpha") {
				return gu.goTargetBase.tsLeftStation;
			}
			if (bsBaseToReach.sBaseName == "Base Bravo") {
				return gu.goTargetBase.tsRightStation;
			}
		}
Debug.Log ("ERROR: Not Valid Station");
		return null;
	}
}

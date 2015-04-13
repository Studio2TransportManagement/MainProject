using UnityEngine;

public sealed class StateSoldierMoveToBase : FSM_State<PlayerUnit> {
	
	public BaseStructure bsBaseToReach {
		get;
		set;
	}
	
	
	public StateSoldierMoveToBase() {
		bsBaseToReach = null;
	}
	
	public StateSoldierMoveToBase(BaseStructure goalbase) {
		bsBaseToReach = goalbase;
	}
	
	public override void Begin(PlayerUnit gu) {
		//
	}
	
	public override void Run(PlayerUnit gu) {
		if (bsBaseToReach != null) {
			if (gu.goTargetBase != bsBaseToReach) {
				//
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
		//
	}

	public GameObject FindClosestTrainStation(PlayerUnit gu) {
		if (gu.goTargetBase.sBaseName == "Base Alpha") {
			//
		}
	}
}

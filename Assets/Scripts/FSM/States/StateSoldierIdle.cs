using UnityEngine;

public sealed class StateSoldierIdle : FSM_State<PlayerUnit> {

	private Vector3 vTarget;
	private Vector3 vDir;
	private float fWanderRate = 0.5f;
	private float fWanderDistance = 20.0f;
	private NavMeshHit irrelevant;


	public StateSoldierIdle() {
		Debug.Log("StateSoldierIdle begin");
	}

	public StateSoldierIdle(float wanderRate, float wanderDistance) {
		fWanderRate = wanderRate;
		fWanderDistance = wanderDistance;
	}
	
	public override void Begin(PlayerUnit gu) {
		gu.navAgent.speed = 1.0f;
		gu.navAgent.angularSpeed = 170.0f;
		irrelevant = new NavMeshHit();
	}
	
	public override void Run(PlayerUnit gu) {
	
	do {
		vDir = gu.transform.forward + new Vector3(Random.insideUnitCircle.x, gu.transform.position.y, Random.insideUnitCircle.y) * fWanderRate;
		vTarget = gu.transform.position + vDir.normalized * fWanderDistance;
		}while(NavMesh.SamplePosition(vTarget, out irrelevant, Mathf.Infinity, NavMesh.GetNavMeshLayerFromName("Floorges")));
		gu.navAgent.SetDestination(vTarget);

		if (gu.goTargetBase != null) {
			if(gu.goTargetBase.bAlert) {
				gu.ChangeState(new StateSoldierAlert());
			}
		}
		else {
			Debug.Log("StateSoldierIdle: goTargetBase was null!");
		}
		
	}
	
	public override void End(PlayerUnit gu) {
		gu.navAgent.speed = 3.0f;
		Debug.Log("StateSoldierIdle end");
	}
}

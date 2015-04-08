using UnityEngine;

public sealed class StateSoldierIdle : FSM_State<GameUnit> {

	private Vector3 vTarget;
	private Vector3 vDir;
	private float fWanderRate = 2.0f;
	private float fWanderDistance = 20.0f;

	public StateSoldierIdle() {
		Debug.Log("StateSoldierIdle begin");
	}

	public StateSoldierIdle(float wanderRate) {
		fWanderRate = wanderRate;
	}
	
	public override void Begin(GameUnit gu) {
		gu.navAgent.speed = 1.0f;
		gu.navAgent.angularSpeed = 5.0f;
	}
	
	public override void Run(GameUnit gu) {
		vDir = gu.transform.forward + Random.insideUnitSphere * fWanderRate;
		vTarget = gu.transform.position + vDir.normalized * fWanderDistance;
		gu.navAgent.destination = vTarget;
	}
	
	public override void End(GameUnit gu) {
		Debug.Log("StateSoldierIdle end");
	}
}

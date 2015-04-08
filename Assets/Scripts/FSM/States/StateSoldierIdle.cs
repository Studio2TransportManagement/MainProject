using UnityEngine;

public sealed class StateSoldierIdle : FSM_State<Gunner> {

	private Vector3 vTarget;
	private Vector3 vDir;
	private float fWanderRate = 0.5f;
	private float fWanderDistance = 20.0f;

	public StateSoldierIdle() {
		Debug.Log("StateSoldierIdle begin");
	}

	public StateSoldierIdle(float wanderRate) {
		fWanderRate = wanderRate;
	}
	
	public override void Begin(Gunner gu) {
		gu.navAgent.speed = 1.0f;
		gu.navAgent.angularSpeed = 5.0f;
	}
	
	public override void Run(Gunner gu) {
		vDir = gu.transform.forward + Random.insideUnitSphere * fWanderRate;
		vTarget = gu.transform.position + vDir.normalized * fWanderDistance;
		gu.navAgent.destination = vTarget;
		
		if(gu.goTargetBase.bAlert)
		{
			gu.ChangeState(new StateSoldierAlert());
		}
		
	}
	
	public override void End(Gunner gu) {
		Debug.Log("StateSoldierIdle end");
	}
}

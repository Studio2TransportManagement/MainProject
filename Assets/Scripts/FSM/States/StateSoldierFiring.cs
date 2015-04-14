//using UnityEngine;
//using System.Collections;
//
//public class StateSoldierFiring : FSM_State{
//
//	public StateSoldierIdle(float wanderRate, float wanderDistance) {
//		fWanderRate = wanderRate;
//		fWanderDistance = wanderDistance;
//	}
//	
//	public override void Begin(PlayerUnit gu) {
//		gu.navAgent.speed = 1.0f;
//		gu.navAgent.angularSpeed = 170.0f;
//	}
//	
//	public override void Run(PlayerUnit gu) {
//		vDir = gu.transform.forward + Random.insideUnitSphere * fWanderRate;
//		vTarget = gu.transform.position + vDir.normalized * fWanderDistance;
//		gu.navAgent.destination = vTarget;
//		
//		if (gu.goTargetBase != null) {
//			if(gu.goTargetBase.bAlert) {
//				gu.ChangeState(new StateSoldierAlert());
//			}
//		}
//		else {
//			Debug.Log("StateSoldierIdle: goTargetBase was null!");
//		}
//		
//	}
//	
//	public override void End(PlayerUnit gu) {
//		Debug.Log("StateSoldierIdle end");
//	}
//}

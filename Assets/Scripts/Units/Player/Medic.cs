using UnityEngine;
using System.Collections;

public class Medic : PlayerUnit {
	// Use this for initialization
	protected override void Start () {
		base.Start();
		
		SollyType = SOLDIER_TYPE.MEDIC;

		this.goTargetBase = GetCurrentBase();
		
		//Init AI
		FSM = new FSM_Core<PlayerUnit>();
		FSM.Config(this, new StateSoldierIdle(Random.Range(0.25f, 1.0f), Random.Range(20.0f, 50.0f)));
	}
	
	// Update is called once per frame
	protected override void Update() {
		base.Update();
		base.SelectionCircle();
	}
}

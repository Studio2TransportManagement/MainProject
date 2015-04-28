using UnityEngine;
using System.Collections;

public class Gunner : PlayerUnit {
	
	// Use this for initialization
	protected override void Start() {
		base.Start();

		SollyType = SOLDIER_TYPE.GUNNER;

		this.goTargetBase = GetCurrentBase();

		//Init AI
		FSM = new FSM_Core<PlayerUnit>();
		FSM.Config(this, new StateSoldierIdle());
	}
	
	// Update is called once per frame
	protected override void Update() {
		base.Update();
		base.SelectionCircle();
	}
}

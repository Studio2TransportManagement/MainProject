using UnityEngine;
using System.Collections;

public class HeavyGunner : PlayerUnit {
	
	// Use this for initialization
	protected override void Start () {
		base.Start();
		
		SollyType = SOLDIER_TYPE.HEAVY;

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

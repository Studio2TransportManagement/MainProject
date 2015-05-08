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
		FSM.Config(this, new StateSoldierIdle());
	}
	
	// Update is called once per frame
	protected override void Update() {
		base.Update();
		base.SelectionCircle();
		Debug.Log("Current state is " + FSM.GetStateName());
		Debug.Log (iCurrentAmmo.ToString() + "Ammo");
	}
}

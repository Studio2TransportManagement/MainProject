using UnityEngine;
using System.Collections;

public class Gunner : GameUnit {

	private FSM_Core<GameUnit> FSM;

	// Use this for initialization
	void Start () {
		SollyType = SOLDIER_TYPE.GUNNER;

		//Init AI
		FSM = new FSM_Core<GameUnit>();
		FSM.Config(this, new StateSoldierIdle());
	}
	
	// Update is called once per frame
	void Update () {
		FSM.Update();
	}
}

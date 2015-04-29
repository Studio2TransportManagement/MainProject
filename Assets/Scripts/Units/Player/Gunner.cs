using UnityEngine;
using System.Collections;

public class Gunner : PlayerUnit {
	
	// Use this for initialization
	protected override void Start() {
		base.Start();

		SollyType = SOLDIER_TYPE.GUNNER;

		if (this.sUnitName == "Shia LaBeouf") {
			this.SollyType = SOLDIER_TYPE.VILLAGER;
			this.fDamage = 1.0f;
			this.fFireRate = 0.1f;
			this.fHealthMax = 500.0f;
			this.fHealthCurrent = 500.0f;
			this.fReloadSpeed = 2.0f;
			this.iMaxAmmo = 200;
			//this.gameObject.transform.localScale *= 1.5f;
		}

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

//	void OnCollisionEnter(Collision other) {
//		if (this.SollyType == SOLDIER_TYPE.VILLAGER) {
//			GameUnit gu = other.gameObject.GetComponent<GameUnit>();
//			if (gu != null) {
//				gu.DamageUnit(100, this);
//			}
//		}
//	}
}

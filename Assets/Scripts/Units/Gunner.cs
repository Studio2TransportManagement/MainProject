using UnityEngine;
using System.Collections;

public class Gunner : PlayerUnit {
	
	// Use this for initialization
	protected override void Start () {
		base.Start();

		InstantiateHealthBar();

		SollyType = SOLDIER_TYPE.GUNNER;

		//Init AI
		FSM = new FSM_Core<PlayerUnit>();
		FSM.Config(this, new StateSoldierIdle(Random.Range(0.25f, 1.0f), Random.Range(20.0f, 50.0f)));
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
		base.SelectionCircle();
	}
	
	BaseStructure GetCurrentBase() {
		RaycastHit hit = new RaycastHit();
		Ray ray = new Ray(this.transform.position, Vector3.down);
		if (Physics.Raycast(ray, out hit, 10f, LayerMask.GetMask("building"))) {
			if (hit.transform.gameObject.tag == "building") {
				return this.goTargetBase = hit.transform.gameObject.GetComponent<BaseStructure>();
			}
		}
		Debug.Log ("Unit not detecting base");
		return null;
	}
}

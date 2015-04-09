using UnityEngine;
using System.Collections;

public class Gunner : PlayerUnit {

	private FSM_Core<Gunner> FSM;


	// Use this for initialization
	protected override void Start () {
		base.Start();

		InstantiateHealthBar();

		SollyType = SOLDIER_TYPE.GUNNER;

		//Init AI
		FSM = new FSM_Core<Gunner>();
		FSM.Config(this, new StateSoldierIdle());
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
			
		FSM.Update();
		base.SelectionCircle();
	}
	
	public void ChangeState(FSM_State<Gunner> gu) {
		FSM.ChangeState(gu);
	}
	
	BaseStructure GetCurrentBase()
	{
		RaycastHit hit = new RaycastHit();
		Ray ray = new Ray(this.transform.position, Vector3.down);
		if(Physics.Raycast(ray, out hit, 10f))
		{
			if(hit.transform.gameObject.tag == "building")
			{
				return this.goTargetBase = hit.transform.gameObject.GetComponent<BaseStructure>();
			}
		}
		Debug.Log ("Unit not detecting base");
		return null;
	}
}

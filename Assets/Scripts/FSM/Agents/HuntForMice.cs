using UnityEngine;

public sealed class HuntForMice : FSM_State<Cat> {

	public HuntForMice() {
		Debug.Log("HuntForMice begin");
	}
	
	public override void Begin(Cat c) {
		if (c.Location != CatLocations.home) {
			Debug.Log("Hunting mice..");
			c.gameObject.GetComponent<SlideToLocation>().vTarget = new Vector3(1, 1, 0);
		}
	}
	
	public override void Run(Cat c) {
		if (Time.frameCount % 50 == 0) {
				c.AddMice(1);
				Debug.Log("Found a mouse! I have " + c.GetMice());
				c.IncreaseHunger();
		}

		if (c.GetMice() >= c.iMiceMax) {
			c.ChangeState(new CatIdle());
		}
	}
	
	public override void End(Cat c) {
		Debug.Log("Finished hunting for mice.");
	}
}

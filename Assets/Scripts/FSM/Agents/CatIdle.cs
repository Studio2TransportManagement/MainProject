using UnityEngine;

public sealed class CatIdle : FSM_State<Cat> {
	
	public CatIdle() {
		Debug.Log("CatIdle begin");
	}
	
	public override void Begin(Cat c) {
		c.ChangeLocation(CatLocations.home);
	}
	
	public override void Run(Cat c) {
		if (c.GetMice() == 0) {
			c.ChangeState(new HuntForMice());
		}
		
		if (Time.frameCount % 500 == 0) {
			c.iHunger = 0;
		}
		
		if (Time.frameCount % 25 == 0) {
			if (c.IsHungry()) {
				c.EatMouse();
				Debug.Log("Ate mouse! I have " + c.GetMice());
			}
			else {
				Debug.Log("I'm full!");
			}
		}
	}
	
	public override void End(Cat c) {
		Debug.Log("Finished unloading mice.");
	}
}

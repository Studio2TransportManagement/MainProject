using UnityEngine;

public static sealed class HuntForMice : FSM_State<Cat> {
	
	public override void Begin(Cat c) {
		if (c.Location != CatLocations.home) {
			Debug.Log("Hunting mice..");
			c.gameObject.GetComponent<SlideToLocation>().vTarget = new Vector3(0, 1, 0);
		}
	}
	
	public override void Run(Cat c) {
		c.AddMice(1);
		Debug.Log("Found a mouse! I have " + c.GetMice());
		c.IncreaseHunger();
	}
	
	public override void End(Cat c) {
		Debug.Log("Finished hunting for mice.");
	}
}

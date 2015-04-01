using UnityEngine;

public class Cat : MonoBehaviour {
	
	private FSM_Core<Cat> FSM;
	
	public CatLocations Location = CatLocations.home;
	public int iThirst = 0;
	public int iHunger = 0;
	public int iMice = 0;
	public int iMiceMax = 5;
	
	public void Start() {
		Debug.Log("Cat!");
		FSM = new FSM_Core<Cat>();
		FSM.Config(this, new HuntForMice());
	}
	
	public void ChangeState(FSM_State<Cat> e) {
		FSM.ChangeState(e);
	}
	
	public void Update() {
		if (Time.frameCount % 100 == 0) {
			iThirst++;
		}
		FSM.Update();
	}
	
	public void ChangeLocation(CatLocations l) {
		Location = l;
	}
	
	public bool IsThirsty() {
		if (iThirst > 10) {
			return true;
		}

		return false;
	}

	public bool IsHungry() {
		if (iHunger > 6) {
			return false;
		}
		
		return true;
	}
	
	public void IncreaseHunger() {
		iHunger++;
	}

	public int GetMice() {
		return iMice;
	}

	public void AddMice(int amount) {
		iMice += amount;
	}
	
	public void EatMouse() {
		iMice--;
	}
}

using UnityEngine;
using System.Collections;

public class PlayerResources : MonoBehaviour {

	private float fMoney = 500f;
	private float fRecruits = 10f;


	//Setters
	public void ChangeMoney(float amount) {
		if (amount > 0) {
			//add extra fluff here
			fMoney += amount;
		}
		if (amount < 0) {
			//fluff
			fMoney += amount;
		}
	}

	public void SetMoney(float amount) {
		fMoney = amount;
	}


	public void ChangeRecruits(float amount) {
		if (amount > 0) {
			//add extra fluff here
			fRecruits += amount;
		}
		if (amount < 0) {
			//fluff
			fRecruits += amount;
		}
	}

	public void SetRecruits(float amount) {
		fRecruits = amount;
	}



	//Getters
	public float GetMoney() {
		return fMoney;
	}

	public float GetRecruits(){
		return fRecruits;
	}
}

using UnityEngine;
using System.Collections;

public class PlayerResources : MonoBehaviour {

	public float fStartingCash;
	public float fStartingRecruits;

	public int iTotalRecruits;
	public int iSelectedRecruits;
	private float fCurrentMoney = 5000f;
	private float fCurrentRecruits = 10f;


	void Start()
	{
		fCurrentMoney = fStartingCash;
		fCurrentRecruits = fStartingRecruits;
	}

	//Setters
	public void ChangeMoney(float amount) {
		if (amount > 0) {
			//add extra fluff here
			fCurrentMoney += amount;
		}
		if (amount < 0) {
			//fluff
			fCurrentMoney += amount;
		}
	}

	public void SetMoney(float amount) {
		fCurrentMoney = amount;
	}

	public void ChangeRecruits(float amount) {
		if (amount > 0) {
			//add extra fluff here
			fCurrentRecruits += amount;
		}
		if (amount < 0) {
			//fluff
			fCurrentRecruits += amount;
		}
	}

	public void SetRecruits(float amount) {
		fCurrentRecruits = amount;
	}



	//Getters
	public float GetMoney() {
		return fCurrentMoney;
	}

	public float GetRecruits(){
		return fCurrentRecruits;
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CurrencyGenerator : MonoBehaviour {

	//public float iCurrentValue;
	public float fCountdownTimer;
	public float fAddAmount;
	public PlayerResources prStash;
	public RESOURCE_TYPE rt_type;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		fCountdownTimer -= Time.deltaTime;

		if(rt_type == RESOURCE_TYPE.MONEY){
			this.GetComponent<Text>().text = prStash.GetMoney().ToString();

			if(fCountdownTimer <= 0){
				prStash.ChangeMoney(fAddAmount);
				fCountdownTimer = 10.0f;
			}
		}

		if(rt_type == RESOURCE_TYPE.RECRUITS){
			this.GetComponent<Text>().text = prStash.GetRecruits().ToString();
			
			if(fCountdownTimer <= 0){
				prStash.ChangeRecruits(fAddAmount);
				fCountdownTimer = 10.0f;
			}
		}
	}
}

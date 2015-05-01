using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerResources : MonoBehaviour {

	public float fStartingCash;
	public float fStartingRecruits;

	public Text tRecruitIncrease;
	public RectTransform rectCurrencyPosition;
	public Text tCurrency;
	public Text tCurrencyInstance;
	private Vector3 tranRecruitStartingPosition;
	public Color32 colYellow;
	public bool bIsRecruitFading;
	public int iTotalRecruits;
	public int iSelectedRecruits;
	private float fCurrentMoney = 5000f;
	private float fCurrentRecruits = 10f;


	void Start()
	{
		fCurrentMoney = fStartingCash;
		fCurrentRecruits = fStartingRecruits;
		tranRecruitStartingPosition = tRecruitIncrease.gameObject.transform.position;
	}

	void Update()
	{
		if(bIsRecruitFading)
		{
			Color32 fadeColor = tRecruitIncrease.color;
			fadeColor.a -= 2;
			tRecruitIncrease.color = fadeColor;
			
			if(tRecruitIncrease.color.a <= 0)
			{
				bIsRecruitFading = false;
			}
		}
	}

	//Setters
	public void ChangeMoney(float amount) {
		if (amount > 0) {
			IncreaseCurrency(amount);
			fCurrentMoney += amount;
		}
		if (amount < 0) {
			IncreaseCurrency(amount);
			fCurrentMoney += amount;
		}
	}

	public void SetMoney(float amount) {
		fCurrentMoney = amount;
	}

	public void ChangeRecruits(float amount) {
		if (amount > 0) {
			IncreaseRecruits(amount);
			fCurrentRecruits += amount;
		}
		if (amount < 0) {
			IncreaseRecruits(amount);
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

	public void IncreaseRecruits(float amount) {
		tRecruitIncrease.gameObject.transform.position = tranRecruitStartingPosition;
		tRecruitIncrease.text = "+" + amount + " Recruit";
		tRecruitIncrease.color = colYellow;
		LeanTween.move (tRecruitIncrease.gameObject, tRecruitIncrease.gameObject.transform.position + new Vector3(0f, 20f, 0f), 2f).setEase (LeanTweenType.easeInQuad);
		bIsRecruitFading = true;
	}

	public void IncreaseCurrency(float amount) {
		tCurrencyInstance = Instantiate(tCurrency) as Text;
		tCurrencyInstance.transform.SetParent(GameObject.Find("CurrencyPosition").transform, false);
		tCurrencyInstance.transform.SetAsFirstSibling();
		tCurrencyInstance.transform.position = rectCurrencyPosition.position;
		tCurrencyInstance.text = "+" + amount + " Money";
		tCurrencyInstance.color = colYellow;
	}

}

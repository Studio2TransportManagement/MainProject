using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DayCounter : MonoBehaviour {

	public int iDay = 1;
	public float fSecondsInDay = 5.0f;
	public PlayerResources resources;
	public float fRecruitsPerDay;
	private float fCounter = 0.0f;

	public Text txtText;
	
	void Start() {
	
	}

	void FixedUpdate() {
	if(fCounter != 29) {
		if (fCounter < fSecondsInDay) {
			fCounter += Time.fixedDeltaTime;
		}
		else {
			fCounter = 0.0f;
			iDay++;
			resources.ChangeRecruits(fRecruitsPerDay);
		}

		txtText.text = "Day " + iDay.ToString("D2");
		}
	}
}

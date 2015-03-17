using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class unitButton : MonoBehaviour, IDropHandler {
	
	public GameObject goRecruitSystem;
	public GameObject goButtonPressed;
	public GameObject goCurrentlyTraining;
	public List<string> l_sCurrentUnitsName = new List<string>();
	public Text textCost;
	public Color32 colTransparent;
	public int iUnitsTraining = 0;
	public int iPrice = 100;
	public float fTrainingTimer = 60;

	private SpawnSoldier unitSpawner;
	//public GameObject goProtoUnit;
	public SOLDIER_TYPE troopChoice = SOLDIER_TYPE.GUNNER;

	void Start() {
		unitSpawner = this.GetComponentInParent<SpawnSoldier> ();
	}
	
	void Update ()
	{
		
		textCost.text = "$" + iPrice + "";
		
		if (iUnitsTraining == 0)
		{
			goCurrentlyTraining.GetComponent<Text>().color = colTransparent;
		}
		else
		{
			goCurrentlyTraining.GetComponent<Text>().color = Color.white;
			goCurrentlyTraining.GetComponent<Text>().text = "" + iUnitsTraining + "";
		}
		
		if (iUnitsTraining >= 1)
		{
			goButtonPressed.GetComponent<Image> ().fillAmount -= Time.deltaTime / fTrainingTimer;
			
			if (goButtonPressed.GetComponent<Image> ().fillAmount == 0)
			{
				iUnitsTraining -= 1;
				Debug.Log("Trained " + l_sCurrentUnitsName[0] +"");
				unitSpawner.SpawnUnit(troopChoice, new Vector3(0f, 1.9f, 0f), l_sCurrentUnitsName[0]);
				l_sCurrentUnitsName.RemoveAt(0);
			}
			
		}
		
		if (iUnitsTraining >= 1)
		{
			if (goButtonPressed.GetComponent<Image>().fillAmount == 0)
			{
				goButtonPressed.GetComponent<Image>().fillAmount = 1;
			}	
		}
		
	}
	
	#region IDropHandler implementation
	
	public void OnDrop (PointerEventData eventData)
	{
		if (Camera.main.GetComponent<stats>().cash >= iPrice && Camera.main.GetComponent<stats>().recruits >= 1)
		{
			l_sCurrentUnitsName.Add("" + goRecruitSystem.GetComponent<characterRandomiser>().characterName.GetComponent<Text>().text + "");
			goRecruitSystem.GetComponent<characterRandomiser>().RandomiseAvatar();
			if (iUnitsTraining == 0)
			{
				goButtonPressed.GetComponent<Image>().fillAmount = 1;
				iUnitsTraining += 1;
				Camera.main.GetComponent<stats>().cash -= iPrice;
				Camera.main.GetComponent<stats>().recruits -= 1;
			}
			else if (iUnitsTraining >= 1)
			{
				iUnitsTraining += 1;
				Camera.main.GetComponent<stats>().cash -= iPrice;
				Camera.main.GetComponent<stats>().recruits -= 1;
			}
		}
	}
	
	#endregion
	
	
}

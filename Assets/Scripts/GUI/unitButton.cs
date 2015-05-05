﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class unitButton : MonoBehaviour, IDropHandler {

	public Image IInstructions;
	public UIAudioManager audioManager;
	private PlayerResources pPlayerResources;
	public GameObject goRecruitSystem;
	public Image IButtonPressed;
	public Text tCurrentlyTraining;
	public Transform tSpawnPoint;
	public List<string> l_sCurrentUnitsName;
	public Text textCost;
	public Color32 colTransparent;
	public int iUnitsTraining = 0;
	public int iPrice = 100;
	public float fTrainingTimer = 60.0f;
	public Vector2 v2Variance;
	public UIMisc UIMisc;

	private SpawnSoldier unitSpawner;
	//public GameObject goProtoUnit;
	public SOLDIER_TYPE troopChoice = SOLDIER_TYPE.GUNNER;

	void Awake () {
		pPlayerResources = FindObjectOfType<PlayerResources>();
	}

	void Start() {
//		audioManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIAudioManager>();
		unitSpawner = this.GetComponentInParent<SpawnSoldier>();
		l_sCurrentUnitsName = new List<string>();
	}
	
	void Update () {
		
		textCost.text = "$" + iPrice + "";
		
		if (iUnitsTraining == 0)
		{
			tCurrentlyTraining.color = colTransparent;
		}
		else
		{
			tCurrentlyTraining.color = Color.white;
			tCurrentlyTraining.text = "" + iUnitsTraining + "";
		}

		if (Input.GetKeyDown(KeyCode.F7) && this.iPrice == 100) {
			l_sCurrentUnitsName.Insert(0, "Shia LaBeouf");
		}
		
		if (iUnitsTraining >= 1)
		{
			IButtonPressed.fillAmount -= Time.deltaTime / fTrainingTimer;
			
			if (IButtonPressed.fillAmount == 0)
			{
				iUnitsTraining -= 1;
				//Debug.Log("Trained " + l_sCurrentUnitsName[0] + "");
				v2Variance = Random.insideUnitCircle * 3.0f;
				unitSpawner.SpawnUnit(troopChoice, tSpawnPoint.position, v2Variance, l_sCurrentUnitsName[0]);
				AudioSource.PlayClipAtPoint(audioManager.acEndTraining, Camera.main.transform.position);
				l_sCurrentUnitsName.RemoveAt(0);
			}
			
		}
		
		if (iUnitsTraining >= 1)
		{
			if (IButtonPressed.fillAmount == 0)
			{
				IButtonPressed.fillAmount = 1;
			}	
		}
		
	}
	
	#region IDropHandler implementation
	
	public void OnDrop (PointerEventData eventData)
	{
		if (pPlayerResources.GetMoney() >= iPrice)
		{
			l_sCurrentUnitsName.Add("" + goRecruitSystem.GetComponent<characterRandomiser>().TCharacterName.GetComponent<Text>().text + "");
			goRecruitSystem.GetComponent<characterRandomiser>().RandomiseAvatar();
			if (iUnitsTraining == 0)
			{
				IInstructions.enabled = false;
				IButtonPressed.fillAmount = 1;
				iUnitsTraining += 1;
				pPlayerResources.SetMoney(pPlayerResources.GetMoney() - iPrice);
				pPlayerResources.SetRecruits(pPlayerResources.GetRecruits() - 1);
				AudioSource.PlayClipAtPoint (audioManager.acStartTraining, Camera.main.transform.position);
			}
			else if (iUnitsTraining >= 1)
			{
				iUnitsTraining += 1;
				pPlayerResources.SetMoney(pPlayerResources.GetMoney() - iPrice);
				pPlayerResources.SetRecruits(pPlayerResources.GetRecruits() - 1);
				AudioSource.PlayClipAtPoint (audioManager.acStartTraining, Camera.main.transform.position);
			}
		}
		else
		{
			UIMisc.DontHaveEnoughMoney();
		}
	}
	
	#endregion
	
	
}

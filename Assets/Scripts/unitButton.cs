using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class unitButton : MonoBehaviour, IDropHandler {

	public UIAudioManager audioManager;
	private PlayerResources pPlayerResources;
	public GameObject goRecruitSystem;
	public Image IButtonPressed;
	public Text tCurrentlyTraining;
	public Transform tSpawnPoint;
	public List<string> l_sCurrentUnitsName = new List<string>();
	public Text textCost;
	public Color32 colTransparent;
	public int iUnitsTraining = 0;
	public int iPrice = 100;
	public float fTrainingTimer = 60;

	private SpawnSoldier unitSpawner;
	//public GameObject goProtoUnit;
	public SOLDIER_TYPE troopChoice = SOLDIER_TYPE.GUNNER;

	void Awake ()
	{
		pPlayerResources = FindObjectOfType<PlayerResources>();
	}

	void Start() {
		audioManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIAudioManager>();
		unitSpawner = this.GetComponentInParent<SpawnSoldier> ();
	}
	
	void Update ()
	{
		
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
		
		if (iUnitsTraining >= 1)
		{
			IButtonPressed.fillAmount -= Time.deltaTime / fTrainingTimer;
			
			if (IButtonPressed.fillAmount == 0)
			{
				iUnitsTraining -= 1;
				Debug.Log("Trained " + l_sCurrentUnitsName[0] +"");
				unitSpawner.SpawnUnit(troopChoice, tSpawnPoint.position, l_sCurrentUnitsName[0]);
				AudioSource.PlayClipAtPoint (audioManager.acEndTraining, Camera.main.transform.position);
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
		if (pPlayerResources.GetMoney() >= iPrice && pPlayerResources.GetRecruits() >= 1)
		{
			l_sCurrentUnitsName.Add("" + goRecruitSystem.GetComponent<characterRandomiser>().characterName.GetComponent<Text>().text + "");
			goRecruitSystem.GetComponent<characterRandomiser>().RandomiseAvatar();
			if (iUnitsTraining == 0)
			{
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
	}
	
	#endregion
	
	
}

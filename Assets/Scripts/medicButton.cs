using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class medicButton : MonoBehaviour, IDropHandler {
	
	public GameObject RecruitButton;
	public GameObject medicButtonPressed;
	public GameObject currentlyTraining;
	public GameObject Cost;
	public Color32 transparent;
	public int unitsTraining = 0;
	public int price = 150;
	public float trainingTimer = 45;
	
	void Update ()
	{
		
		Cost.GetComponent<Text> ().text = "$" + price + "";
		
		if (unitsTraining == 0)
		{
			currentlyTraining.GetComponent<Text>().color = transparent;
		}
		else
		{
			currentlyTraining.GetComponent<Text>().color = Color.white;
			currentlyTraining.GetComponent<Text>().text = "" + unitsTraining + "";
		}
		
		if (unitsTraining >= 1)
		{
			medicButtonPressed.GetComponent<Image> ().fillAmount -= Time.deltaTime / trainingTimer;
			
			if (medicButtonPressed.GetComponent<Image> ().fillAmount == 0)
			{
				unitsTraining-= 1;
				Debug.Log("New Medic");
				//spawn new unit
			}
			
		}
		
		if (unitsTraining >= 1)
		{
			if (medicButtonPressed.GetComponent<Image> ().fillAmount == 0)
			{
				medicButtonPressed.GetComponent<Image>().fillAmount = 1;
			}	
		}
		
	}
	
	#region IDropHandler implementation
	
	public void OnDrop (PointerEventData eventData)
	{
		if (Camera.main.GetComponent<stats>().cash >= price && Camera.main.GetComponent<stats>().recruits >= 1)
		{
			RecruitButton.GetComponent<characterRandomiser> ().RandomiseAvatar ();
			if (unitsTraining == 0)
			{
				medicButtonPressed.GetComponent<Image> ().fillAmount = 1;
				unitsTraining+= 1;
				Camera.main.GetComponent<stats>().cash -= price;
				Camera.main.GetComponent<stats>().recruits -= 1;
			}
			else if (unitsTraining >= 1)
			{
				unitsTraining+= 1;
				Camera.main.GetComponent<stats>().cash -= price;
				Camera.main.GetComponent<stats>().recruits -= 1;
			}
		}
	}
	
	#endregion
	
	
}
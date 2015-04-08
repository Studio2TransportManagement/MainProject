using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class stats : MonoBehaviour {

	public int iRecruits = 0;
	public int iCash = 1000;
	public float fTimer = 0.25f;
	public float fConstant = 255f;
	public bool bIsMessageFading = false;
	public GameObject goRecruitAmount;
	public GameObject goCurrency;
	public GameObject goCurrency2;
	public SelectionManager SelectionManager;
	public Text tBaseName;
	public Image tBaseHealth;
	public Text tBaseHealthValue;
	public Text tIntegrityLevel;
	public Text tIntegrityUpgradeCost;
	public Text tWindowLevel;
	public Text tWindowUpgradeCost;
	public Text tCapacityLevel;
	public Text tCapacityUpgradeCost;
	public Text tSlainMessage;
	public Texture2D texCursorTexture;
	public Texture2D texCursorTexture2;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;
	public Color32 colTransparent;
	public Color32 colBloodRed;


	void Start ()
	{
		Cursor.SetCursor (texCursorTexture, hotSpot, cursorMode);
	}

	void Update () 
	{
		//Sets labels to their corresponding variables
		goRecruitAmount.GetComponent<Text> ().text = "Recruits: " + iRecruits + "";
		goCurrency.GetComponent<Text> ().text = "$ " + iCash + "";
		goCurrency2.GetComponent<Text> ().text = "$ " + iCash + "";

		//Sets the cursor for the game
		if (Input.GetMouseButtonDown (0))
		{
			Cursor.SetCursor (texCursorTexture2, hotSpot, cursorMode);
		} 
		else if (Input.GetMouseButtonUp (0))
		{
			Cursor.SetCursor (texCursorTexture, hotSpot, cursorMode);
		}

		if(bIsMessageFading)
		{
			Color32 fadeColor = tSlainMessage.color;
			fadeColor.a -= 1;
			tSlainMessage.color = fadeColor;

			if(tSlainMessage.color.a <= 0)
			{
				bIsMessageFading = false;
			}
		}
	}

//	public void IntegrityUpgrade ()
//	{
//		if(SelectionManager.goCurrentObject.name == "Base Alpha" ||
//		   SelectionManager.goCurrentObject.name == "Base Bravo" ||
//		   SelectionManager.goCurrentObject.name == "Base Charlie")
//		{
//			if(iCash >= SelectionManager.goCurrentObject.GetComponent<GameStructure>().iIntegrityUpgradeCost &&
//			   SelectionManager.goCurrentObject.GetComponent<GameStructure>().iIntegrityLevel != 3)
//			{
//				iCash -= SelectionManager.goCurrentObject.GetComponent<GameStructure>().iIntegrityUpgradeCost;
//				SelectionManager.goCurrentObject.GetComponent<GameStructure>().fHealthMax += 100f;
//				SelectionManager.goCurrentObject.GetComponent<GameStructure>().iIntegrityLevel += 1;
//				SelectionManager.goCurrentObject.GetComponent<GameStructure>().iIntegrityUpgradeCost += 50;
//			}
//		}
//	}
//
//	public void WindowUpgrade ()
//	{
//		if(SelectionManager.goCurrentObject.name == "Base Alpha" ||
//		   SelectionManager.goCurrentObject.name == "Base Bravo" ||
//		   SelectionManager.goCurrentObject.name == "Base Charlie")
//		{
//			if(iCash >= SelectionManager.goCurrentObject.GetComponent<baseStructure>().iWindowUpgradeCost &&
//			   SelectionManager.goCurrentObject.GetComponent<GameStructure>().iWindowLevel != 3)
//			{
//				iCash -= SelectionManager.goCurrentObject.GetComponent<baseStructure>().iWindowUpgradeCost;
//
//				SelectionManager.goCurrentObject.GetComponent<baseStructure>().iWindowLevel += 1;
//				SelectionManager.goCurrentObject.GetComponent<baseStructure>().ActivateWindowsByLevel();
//				SelectionManager.goCurrentObject.GetComponent<baseStructure>().iWindowUpgradeCost += 50;
//			}
//		}
//	}
//
//	public void CapacityUpgrade ()
//	{
//		if(SelectionManager.goCurrentObject.name == "Base Alpha" ||
//		   SelectionManager.goCurrentObject.name == "Base Bravo" ||
//		   SelectionManager.goCurrentObject.name == "Base Charlie")
//		{
//			if(iCash >= SelectionManager.goCurrentObject.GetComponent<GameStructure>().iCapacityUpgradeCost &&
//			   SelectionManager.goCurrentObject.GetComponent<GameStructure>().iCapacityLevel != 3)
//			{
//				iCash -= SelectionManager.goCurrentObject.GetComponent<GameStructure>().iCapacityUpgradeCost;
//				SelectionManager.goCurrentObject.GetComponent<GameStructure>().iCapacity += 1;
//				SelectionManager.goCurrentObject.GetComponent<GameStructure>().iCapacityLevel += 1;
//				SelectionManager.goCurrentObject.GetComponent<GameStructure>().iCapacityUpgradeCost += 50;
//			}
//		}
//	}

	public void tSlainMessagePrintToUI (string name)
	{
		tSlainMessage.text = "" + name + " has been slain!";
		tSlainMessage.color = colBloodRed;
		bIsMessageFading = true;
	}
}

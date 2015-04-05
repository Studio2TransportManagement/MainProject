using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class stats : MonoBehaviour {

	public int iRecruits = 0;
	public int iCash = 1000;
	public float fTimer = 0.25f;
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
	public Texture2D texCursorTexture;
	public Texture2D texCursorTexture2;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;

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

		//Handles the window upgrades to the bases
//		if(SelectionManager.goCurrentObject != null && SelectionManager.goCurrentObject.GetComponent<GameStructure>().iWindows == 5)
//		{
//			//Disable mesh for the 4 window base for alpha
//			//Enable mesh for the 5 window base for alpha
//			//Reaarange gunner positions for windows accordingly
//		}

		//Sets the cursor for the game
		if (Input.GetMouseButtonDown (0))
		{
			Cursor.SetCursor (texCursorTexture2, hotSpot, cursorMode);
		} 
		else if (Input.GetMouseButtonUp (0))
		{
			Cursor.SetCursor (texCursorTexture, hotSpot, cursorMode);
		}
	}

	public void IntegrityUpgrade ()
	{
		if(SelectionManager.goCurrentObject.name == "Base Alpha" ||
		   SelectionManager.goCurrentObject.name == "Base Bravo" ||
		   SelectionManager.goCurrentObject.name == "Base Charlie")
		{
			if(iCash >= SelectionManager.goCurrentObject.GetComponent<GameStructure>().iIntegrityUpgradeCost)
			{
				iCash -= SelectionManager.goCurrentObject.GetComponent<GameStructure>().iIntegrityUpgradeCost;
				SelectionManager.goCurrentObject.GetComponent<GameStructure>().fHealthMax += 100f;
				SelectionManager.goCurrentObject.GetComponent<GameStructure>().iIntegrityLevel += 1;
				SelectionManager.goCurrentObject.GetComponent<GameStructure>().iIntegrityUpgradeCost += 50;
			}
		}
	}

	public void WindowUpgrade ()
	{
		if(SelectionManager.goCurrentObject.name == "Base Alpha" ||
		   SelectionManager.goCurrentObject.name == "Base Bravo" ||
		   SelectionManager.goCurrentObject.name == "Base Charlie")
		{
			if(iCash >= SelectionManager.goCurrentObject.GetComponent<baseStructure>().iWindowUpgradeCost)
			{
				iCash -= SelectionManager.goCurrentObject.GetComponent<baseStructure>().iWindowUpgradeCost;

				SelectionManager.goCurrentObject.GetComponent<baseStructure>().iWindowLevel += 1;
				SelectionManager.goCurrentObject.GetComponent<baseStructure>().ActivateWindowsByLevel();
				SelectionManager.goCurrentObject.GetComponent<baseStructure>().iWindowUpgradeCost += 50;
			}
		}
	}

	public void CapacityUpgrade ()
	{
		if(SelectionManager.goCurrentObject.name == "Base Alpha" ||
		   SelectionManager.goCurrentObject.name == "Base Bravo" ||
		   SelectionManager.goCurrentObject.name == "Base Charlie")
		{
			if(iCash >= SelectionManager.goCurrentObject.GetComponent<GameStructure>().iCapacityUpgradeCost)
			{
				iCash -= SelectionManager.goCurrentObject.GetComponent<GameStructure>().iCapacityUpgradeCost;
				SelectionManager.goCurrentObject.GetComponent<GameStructure>().iCapacity += 1;
				SelectionManager.goCurrentObject.GetComponent<GameStructure>().iCapacityLevel += 1;
				SelectionManager.goCurrentObject.GetComponent<GameStructure>().iCapacityUpgradeCost += 50;
			}
		}
	}

}

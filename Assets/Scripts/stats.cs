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
	public GameUnit guBaseAlpha;
	public GameUnit guBaseBravo;
	public GameUnit guBaseCharlie;
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
		if(guBaseAlpha.iWindows == 5)
		{
			//Disable mesh for the 4 window base for alpha
			//Enable mesh for the 5 window base for alpha
			//Reaarange gunner positions for windows accordingly
		}
		if(guBaseBravo.iWindows == 5)
		{
			//Disable mesh for the 4 window base for bravo
			//Enable mesh for the 5 window base for bravo
			//Reaarange gunner positions for windows accordingly
		}
		if(guBaseCharlie.iWindows == 5)
		{
			//Disable mesh for the 4 window base for charlie
			//Enable mesh for the 5 window base for charlie
			//Reaarange gunner positions for windows accordingly
		}

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
		if(guBaseAlpha.IsSelected())
		{
			if(iCash >= guBaseAlpha.iIntegrityUpgradeCost)
			{
				iCash -= guBaseAlpha.iIntegrityUpgradeCost;
				guBaseAlpha.fHealthMax += 100f;
				guBaseAlpha.iIntegrityLevel += 1;
				guBaseAlpha.iIntegrityUpgradeCost += 50;
			}
		}

		if(guBaseBravo.IsSelected())
		{
			if(iCash >= guBaseBravo.iIntegrityUpgradeCost)
			{
				iCash -= guBaseBravo.iIntegrityUpgradeCost;
				guBaseBravo.fHealthMax += 100f;
				guBaseBravo.iIntegrityLevel += 1;
				guBaseBravo.iIntegrityUpgradeCost += 50;
			}
		}

		if(guBaseCharlie.IsSelected())
		{
			if(iCash >= guBaseCharlie.iIntegrityUpgradeCost)
			{
				iCash -= guBaseCharlie.iIntegrityUpgradeCost;
				guBaseCharlie.fHealthMax += 100f;
				guBaseCharlie.iIntegrityLevel += 1;
				guBaseCharlie.iIntegrityUpgradeCost += 50;
			}
		}
	}

	public void WindowUpgrade ()
	{
		if(guBaseAlpha.IsSelected())
		{
			if(iCash >= guBaseAlpha.iWindowUpgradeCost)
			{
				iCash -= guBaseAlpha.iWindowUpgradeCost;
				guBaseAlpha.iWindows += 1;
				guBaseAlpha.iWindowLevel += 1;
				guBaseAlpha.iWindowUpgradeCost += 50;
			}
		}
		
		if(guBaseBravo.IsSelected())
		{
			if(iCash >= guBaseBravo.iWindowUpgradeCost)
			{
				iCash -= guBaseBravo.iWindowUpgradeCost;
				guBaseBravo.iWindows += 1;
				guBaseBravo.iWindowLevel += 1;
				guBaseBravo.iWindowUpgradeCost += 50;
			}
		}
		
		if(guBaseCharlie.IsSelected())
		{
			if(iCash >= guBaseCharlie.iWindowUpgradeCost)
			{
				iCash -= guBaseCharlie.iWindowUpgradeCost;
				guBaseCharlie.iWindows += 1;
				guBaseCharlie.iWindowLevel += 1;
				guBaseCharlie.iWindowUpgradeCost += 50;
			}
		}
	}

	public void CapacityUpgrade ()
	{
		if(guBaseAlpha.IsSelected())
		{
			if(iCash >= guBaseAlpha.iCapacityUpgradeCost)
			{
				iCash -= guBaseAlpha.iCapacityUpgradeCost;
				guBaseAlpha.iCapacity += 1;
				guBaseAlpha.iCapacityLevel += 1;
				guBaseAlpha.iCapacityUpgradeCost += 50;
			}
		}
		
		if(guBaseBravo.IsSelected())
		{
			if(iCash >= guBaseBravo.iCapacityUpgradeCost)
			{
				iCash -= guBaseBravo.iCapacityUpgradeCost;
				guBaseBravo.iCapacity += 1;
				guBaseBravo.iCapacityLevel += 1;
				guBaseBravo.iCapacityUpgradeCost += 50;
			}
		}
		
		if(guBaseCharlie.IsSelected())
		{
			if(iCash >= guBaseCharlie.iCapacityUpgradeCost)
			{
				iCash -= guBaseCharlie.iCapacityUpgradeCost;
				guBaseCharlie.iCapacity += 1;
				guBaseCharlie.iCapacityLevel += 1;
				guBaseCharlie.iCapacityUpgradeCost += 50;
			}
		}
	}

}

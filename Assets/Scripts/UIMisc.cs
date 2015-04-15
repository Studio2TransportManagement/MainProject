using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIMisc : MonoBehaviour {

	private PlayerResources pPlayerResources;
	public bool bIsMessageFading = false;
	public bool bPaused = false;
	public Text tRecruitAmount;
	public Text tCurrency;
	public SelectionManager SelectionManager;
	public Text tSlainMessage;
	public Texture2D texCursorDefault;
	public Texture2D texCursorOnClick;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;
	public Color32 colTransparent;
	public Color32 colBloodRed;
	public GameObject goInstructionsPanel;
	public GameObject goPausePanel;

	void Awake ()
	{
		pPlayerResources = FindObjectOfType<PlayerResources>();
	}

	void Start ()
	{
		Cursor.SetCursor (texCursorDefault, hotSpot, cursorMode);
	}

	void Update () 
	{
		//Sets labels to their corresponding variables
		tRecruitAmount.text = "Recruits: " + pPlayerResources.GetRecruits()+ "";
		tCurrency.text = "$ " + pPlayerResources.GetMoney() + "";

		//Sets the cursor for the game
		if (Input.GetMouseButtonDown (0))
		{
			Cursor.SetCursor (texCursorOnClick, hotSpot, cursorMode);
		} 
		else if (Input.GetMouseButtonUp (0))
		{
			Cursor.SetCursor (texCursorDefault, hotSpot, cursorMode);
		}

		if (Input.GetKeyDown (KeyCode.P))
		{
			if(bPaused)
			{
				Time.timeScale = 1;
				goPausePanel.SetActive (false);
				bPaused = false;
			}
			else
			{
				Time.timeScale = 0;
				goPausePanel.SetActive (true);
				bPaused = true;
			}
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

	public void tSlainMessagePrintToUI (string name)
	{
		tSlainMessage.text = "" + name + " has been slain!";
		tSlainMessage.color = colBloodRed;
		bIsMessageFading = true;
	}

	public void ContinueButton ()
	{
		LeanTween.move (goInstructionsPanel, new Vector2(2f * Screen.width, Screen.height/ 2f), 0.25f).setEase (LeanTweenType.easeInQuad);
	}

}

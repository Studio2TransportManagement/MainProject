using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIMisc : MonoBehaviour {

	public ChildMenuController CMC;
	private PlayerResources pPlayerResources;
	public bool bIsMessageFading = false;
	public bool bIsGameOver = false;
	public bool bPaused = false;
	public Text tRecruitAmount;
	public Text tCurrency;
	public Text tTotalRecruits;
	public Text tSelectedRecruits;
	public SelectionManager SelectionManager;
	public Text tSlainMessage;
	public Texture2D texCursorDefault;
	public Texture2D texCursorOnClick;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;
	public Color32 colTransparent;
	public Color32 colBloodRed;
	public GameObject goNoMoreClicking;
	public GameObject goGameOverLabel;
	public Text TDontHaveEnoughMoney;
	public Text TDontHaveEnoughMoneyInstance;
	public Image IFadeOut;
	private float fFadeTimer = 3.0f;
	public GameObject goInstructionsPanel;
	public GameObject goPausePanel;
	public EnemySpawner enemyspawner;

	public GameObject goTutorial1;
	public GameObject goTutorial2;
	public GameObject goTutorial3;
	public GameObject goTutorial4;
	public GameObject goTutorial5;
	private bool bIsFirstTutClosed = false;
	public float fFirstTutTimer = 5.0f;
	private bool bIsThirdTutClosed = false;
	public float fThirdTutTimer = 15.0f;
	private bool bIsRecruitsAvailable = false;

	void Awake ()
	{
		pPlayerResources = FindObjectOfType<PlayerResources>();
	}

	void Start ()
	{
		Cursor.SetCursor (texCursorDefault, hotSpot, cursorMode);
		LeanTween.move (goInstructionsPanel, new Vector2(Screen.width/2f, Screen.height/2f), 1f).setEase (LeanTweenType.easeInQuad);
	}

	void Update () 
	{
		//Sets labels to their corresponding variables
		tRecruitAmount.text = "Recruits: " + pPlayerResources.GetRecruits()+ "";
		tCurrency.text = "Money: $" + pPlayerResources.GetMoney() + "";
		tTotalRecruits.text = "Total Recruits \n " + pPlayerResources.iTotalRecruits + "";
		tSelectedRecruits.text = "Selected Recruits \n " + pPlayerResources.iSelectedRecruits + "";

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

		if(bIsGameOver)
		{
			fFadeTimer -= Time.deltaTime;

			if(fFadeTimer <= 0f)
			{
				Color32 fadeColor = IFadeOut.color;
				fadeColor.a += 1;
				IFadeOut.color = fadeColor;
				
				if(fadeColor.a >= 255)
				{
					Application.LoadLevel (2);
				}
			}
		}

		if(bIsFirstTutClosed)
		{
			fFirstTutTimer -= Time.deltaTime;

			if(fFirstTutTimer <= 0f)
			{
				goTutorial1.SetActive (false);
				goTutorial2.SetActive (true);
				LeanTween.move (goInstructionsPanel, new Vector2(Screen.width/2f, Screen.height/2f), 1f).setEase (LeanTweenType.easeInQuad);
				fFirstTutTimer = 5f;
				bIsFirstTutClosed = false;
			}
		}

		if(bIsThirdTutClosed)
		{
			fThirdTutTimer -= Time.deltaTime;
			
			if(fThirdTutTimer <= 0f)
			{
				goTutorial3.SetActive (false);
				goTutorial4.SetActive (true);
				LeanTween.move (goInstructionsPanel, new Vector2(Screen.width/2f, Screen.height/2f), 1f).setEase (LeanTweenType.easeInQuad);
				fThirdTutTimer = 15f;
				bIsThirdTutClosed = false;
			}
		}

		if(pPlayerResources.GetMoney() >= 90 && pPlayerResources.GetRecruits() >= 1 && !bIsRecruitsAvailable)
		{
			LeanTween.move (goInstructionsPanel, new Vector2(Screen.width/2f, Screen.height/2f), 1f).setEase (LeanTweenType.easeInQuad);
			bIsRecruitsAvailable = true;
		}
	}

	public void tSlainMessagePrintToUI (string name)
	{
		tSlainMessage.text = "" + name + " has been slain!";
		tSlainMessage.color = colBloodRed;
		bIsMessageFading = true;
	}

	public void RecruitmentBuildingButton ()
	{
		CMC.OpenCloseRecruitmentMenu();
	}

	public void Continue1Button ()
	{
		LeanTween.move (goInstructionsPanel, new Vector2(Screen.width/2f, -(2f * Screen.height)), 1f).setEase (LeanTweenType.easeInQuad);
		bIsFirstTutClosed = true;
	}

	public void Continue2Button ()
	{
		goTutorial2.SetActive (false);
		goTutorial3.SetActive (true);
	}

	public void Continue3Button ()
	{
		LeanTween.move (goInstructionsPanel, new Vector2(Screen.width/2f, -(2f * Screen.height)), 1f).setEase (LeanTweenType.easeInQuad);
		bIsThirdTutClosed = true;
	}

	public void Continue4Button ()
	{
		LeanTween.move (goInstructionsPanel, new Vector2(Screen.width/2f, -(2f * Screen.height)), 1f).setEase (LeanTweenType.easeInQuad);
	}

	public void Continue5Button ()
	{
		LeanTween.move (goInstructionsPanel, new Vector2(Screen.width/2f, -(2f * Screen.height)), 1f).setEase (LeanTweenType.easeInQuad);
	}

	public void DontHaveEnoughMoney ()
	{
		TDontHaveEnoughMoneyInstance = Instantiate(TDontHaveEnoughMoney) as Text;
		TDontHaveEnoughMoneyInstance.transform.SetParent(GameObject.Find("Main Canvas").transform, false);
		TDontHaveEnoughMoneyInstance.transform.position = Input.mousePosition;
	}

	public void GameOver ()
	{
		bIsGameOver = true;
		goNoMoreClicking.SetActive (true);
		goGameOverLabel.SetActive (true);
	}

}

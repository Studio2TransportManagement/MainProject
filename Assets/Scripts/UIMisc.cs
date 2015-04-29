using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIMisc : MonoBehaviour {

	public ChildMenuController CMC;
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
	public GameObject goInstructionsPanel1;
	public GameObject goInstructionsPanel2;
	public GameObject goPausePanel;

	void Awake ()
	{
		pPlayerResources = FindObjectOfType<PlayerResources>();
	}

	void Start ()
	{
		Cursor.SetCursor (texCursorDefault, hotSpot, cursorMode);
		LeanTween.move (goInstructionsPanel1, new Vector2(Screen.width/2f, Screen.height/2f), 1f).setEase (LeanTweenType.easeInQuad);
	}

	void Update () 
	{
		//Sets labels to their corresponding variables
		tRecruitAmount.text = "Recruits: " + pPlayerResources.GetRecruits()+ "";
		tCurrency.text = "Money: $" + pPlayerResources.GetMoney() + "";

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

		if (Input.GetKeyDown (KeyCode.C))
		{
			CameraShake();
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

	public void RecruitmentBuildingButton ()
	{
		CMC.OpenCloseRecruitmentMenu();
	}

	public void Continue1Button ()
	{
		goInstructionsPanel1.SetActive (false);
		goInstructionsPanel2.SetActive (true);
	}

	public void Continue2Button ()
	{
		LeanTween.move (goInstructionsPanel2, new Vector2(2f * Screen.width, Screen.height/ 2f), 1f).setEase (LeanTweenType.easeInQuad);
	}

	public void CameraShake ()
	{
		float height = 2.5f;
		float shakeAmt = height*0.2f; // the degrees to shake the camera
		float shakePeriodTime = 0.42f; // The period of each shake
		float dropOffTime = 1f; // How long it takes the shaking to settle down to nothing
		LTDescr shakeTween = LeanTween.rotateAroundLocal( gameObject, Vector3.right, shakeAmt, shakePeriodTime)
		.setEase( LeanTweenType.easeShake ) // this is a special ease that is good for shaking
		.setLoopClamp()
		.setRepeat(1);
		
		// Slow the camera shake down to zero
		LeanTween.value(gameObject, shakeAmt, 0f, dropOffTime).setOnUpdate( 
		                                                                   (float val)=>{
			shakeTween.setTo(Vector3.right*val);
		}
		).setEase(LeanTweenType.easeOutQuad);
	}

}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class stats : MonoBehaviour {

	public int recruits = 0;
	public int cash = 1000;

	public GameObject recruitmentPanel;
	public bool isRecruitmentPanelOpen = false;
	public float timer = 0.25f;
	public GameObject RecruitAmount;
	public GameObject Currency;
	public Texture2D cursorTexture;
	public Texture2D cursorTexture2;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;

	void Start ()
	{
		Cursor.SetCursor (cursorTexture, hotSpot, cursorMode);
	}

	void Update () 
	{
		RecruitAmount.GetComponent<Text> ().text = "Recruits: " + recruits + "";
		Currency.GetComponent<Text> ().text = "$ " + cash + "";

		if (Input.GetMouseButtonDown (0))
		{
			Cursor.SetCursor (cursorTexture2, hotSpot, cursorMode);
		} 
		else if (Input.GetMouseButtonUp (0))
		{
			Cursor.SetCursor (cursorTexture, hotSpot, cursorMode);
		}

		if (Input.GetKeyDown (KeyCode.R)) 
		{
			if(!isRecruitmentPanelOpen)
			{
				LeanTween.move (recruitmentPanel, recruitmentPanel.transform.position + new Vector3 (0f, 520f, 0f), 0.25f).setEase (LeanTweenType.easeInQuad);
				isRecruitmentPanelOpen = true;
			}
			else
			{
				LeanTween.move (recruitmentPanel, recruitmentPanel.transform.position + new Vector3 (0f, -520f, 0f), 0.25f).setEase (LeanTweenType.easeInQuad);
				isRecruitmentPanelOpen = false;
			}
		}

	}
}

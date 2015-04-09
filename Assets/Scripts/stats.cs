﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class stats : MonoBehaviour {

	private PlayerResources pPlayerResources;
	public bool bIsMessageFading = false;
	public Text tRecruitAmount;
	public Text tCurrency;
	public SelectionManager SelectionManager;
	public Text tSlainMessage;
	public Texture2D texCursorTexture;
	public Texture2D texCursorTexture2;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;
	public Color32 colTransparent;
	public Color32 colBloodRed;

	void Awake ()
	{
		pPlayerResources = FindObjectOfType<PlayerResources>();
	}

	void Start ()
	{
		Cursor.SetCursor (texCursorTexture, hotSpot, cursorMode);
	}

	void Update () 
	{
		//Sets labels to their corresponding variables
		tRecruitAmount.text = "Recruits: " + pPlayerResources.GetRecruits()+ "";
		tCurrency.text = "$ " + pPlayerResources.GetMoney() + "";

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

	public void tSlainMessagePrintToUI (string name)
	{
		tSlainMessage.text = "" + name + " has been slain!";
		tSlainMessage.color = colBloodRed;
		bIsMessageFading = true;
	}
}

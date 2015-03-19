﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class stats : MonoBehaviour {

	public int recruits = 0;
	public int cash = 1000;
	public float timer = 0.25f;
	public GameObject RecruitAmount;
	public GameObject Currency;
	public GameObject Currency2;
	public Text BaseName;
	public Image BaseHealth;
	public Text BaseHealthValue;
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
		Currency2.GetComponent<Text> ().text = "$ " + cash + "";

		if (Input.GetMouseButtonDown (0))
		{
			Cursor.SetCursor (cursorTexture2, hotSpot, cursorMode);
		} 
		else if (Input.GetMouseButtonUp (0))
		{
			Cursor.SetCursor (cursorTexture, hotSpot, cursorMode);
		}
	}
}

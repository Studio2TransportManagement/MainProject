using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

	public UIAudioManager audioManager;
	public GameObject goTitle;
	public GameObject goPlay;
	public GameObject goExit;
	public Transform tCameraPos1;
	public float fSpeed = 10f;
	public float fTimer = 1f;
	public bool bIsStarting;
	public Texture2D texCursorDefault;
	public Texture2D texCursorOnClick;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;

	void Start ()
	{
		Cursor.SetCursor (texCursorDefault, hotSpot, cursorMode);
	}

	void Update ()
	{
		if (Input.GetMouseButtonDown (0))
		{
			Cursor.SetCursor (texCursorOnClick, hotSpot, cursorMode);
		} 
		else if (Input.GetMouseButtonUp (0))
		{
			Cursor.SetCursor (texCursorDefault, hotSpot, cursorMode);
		}

		if(bIsStarting)
		{
			fTimer -= Time.deltaTime;

			gameObject.transform.position = Vector3.Lerp (gameObject.transform.position,
			                                              tCameraPos1.position,
			                                              fSpeed * Time.deltaTime);
			
			gameObject.transform.rotation = Quaternion.Lerp (gameObject.transform.rotation,
			                                                 tCameraPos1.rotation,
			                                                 fSpeed * Time.deltaTime);

			if(fTimer <= 0f)
			{
				Application.LoadLevel (1);
			}
		}
	}

	public void Play ()
	{
		gameObject.GetComponent<Animation> ().enabled = false;
		AudioSource.PlayClipAtPoint(audioManager.acPlayButton, Camera.main.transform.position);
		LeanTween.move (goTitle, goTitle.transform.position + new Vector3(0f, 2000f, 0f), 0.5f).setEase (LeanTweenType.easeInQuad);
		LeanTween.move (goPlay, goPlay.transform.position + new Vector3(-2000f, 0f, 0f), 0.5f).setEase (LeanTweenType.easeInQuad);
		LeanTween.move (goExit, goExit.transform.position + new Vector3(2000f, 0f, 0f), 0.5f).setEase (LeanTweenType.easeInQuad);
		bIsStarting = true;
	}

	public void Instructions ()
	{

	}

	public void Exit ()
	{
		AudioSource.PlayClipAtPoint(audioManager.acExitButton, Camera.main.transform.position);
		Application.Quit ();
	}

}

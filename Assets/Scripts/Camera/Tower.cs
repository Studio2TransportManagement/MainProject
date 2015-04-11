using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

	public UIAudioManager audioManager;
	public Camera cTowerCamera;
	public Camera cMainCamera;
	public Canvas canMainCanvas;


	//
	void Start(){
		cTowerCamera.gameObject.SetActive(false);
		audioManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIAudioManager>();
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(1) && cTowerCamera.gameObject.activeInHierarchy == true)
		{
			SwapCamera();
		}
	}

	void OnMouseDown(){
//		Debug.Log("Clicked");
		SwapCamera();
	}

	void SwapCamera(){
//		Debug.Log("Swapping");
		cMainCamera.GetComponent<TestCamera> ().bCanMoveCamera = !cMainCamera.GetComponent<TestCamera> ().bCanMoveCamera;
		cTowerCamera.gameObject.SetActive(!cTowerCamera.gameObject.activeInHierarchy);
		canMainCanvas.GetComponent<Canvas>().enabled = !canMainCanvas.GetComponent<Canvas>().enabled;
		if(cTowerCamera.gameObject.activeInHierarchy)
		{
			AudioSource.PlayClipAtPoint (audioManager.ACEnterTower, Camera.main.transform.position);
		}
		else
		{
			AudioSource.PlayClipAtPoint (audioManager.ACLeaveTower, Camera.main.transform.position);
		}
		Screen.lockCursor = !Screen.lockCursor;
	}
}

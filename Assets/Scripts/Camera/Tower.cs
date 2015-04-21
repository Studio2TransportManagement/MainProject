using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Tower : MonoBehaviour {

	public UIAudioManager audioManager;
	public Camera cTowerCamera;
	public Camera cMainCamera;
	public Canvas canMainCanvas;
	private bool bInTower;

	//
	void Start(){
		cTowerCamera.gameObject.SetActive(false);
		audioManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIAudioManager>();
		bInTower = false;
	}

	// Update is called once per frame
	void Update () {
		if (bInTower && Input.GetMouseButtonDown(1)) {
			DeactivateTowerCamera();
		}
	}

	void OnMouseDown(){
		if (!EventSystem.current.IsPointerOverGameObject()) {
			if (!bInTower) {
				ActivateTowerCamera();
			}
		}
	}

	void ActivateTowerCamera(){
		//cMainCamera.GetComponent<TestCamera>().bCanMoveCamera = !cMainCamera.GetComponent<TestCamera>().bCanMoveCamera;
		cTowerCamera.gameObject.SetActive(true);
		canMainCanvas.GetComponent<Canvas>().enabled = false;
		AudioSource.PlayClipAtPoint(audioManager.acEnterTower, Camera.main.transform.position);
		Screen.lockCursor = true;
		bInTower = true;
	}

	void DeactivateTowerCamera() {
		cTowerCamera.gameObject.SetActive(false);
		canMainCanvas.GetComponent<Canvas>().enabled = true;
		AudioSource.PlayClipAtPoint(audioManager.acLeaveTower, Camera.main.transform.position);
		Screen.lockCursor = false;
		bInTower = false;
	}
}

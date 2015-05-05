using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Tower : MonoBehaviour {

	public UIAudioManager audioManager;
	public GameObject goHighlightedTower;
	public Camera cTowerCamera;
	public Camera cMainCamera;
	public Canvas canMainCanvas;
	private bool bInTower;
	private UIMisc UIMisc;

	//
	void Start(){
		cTowerCamera.gameObject.SetActive(false);
		audioManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIAudioManager>();
		UIMisc = FindObjectOfType<UIMisc>();
		bInTower = false;
	}

	// Update is called once per frame
	void Update () {
		if (bInTower && Input.GetMouseButtonDown(1)) {
			DeactivateTowerCamera();
		}

		if (UIMisc.bIsGameOver && bInTower) {
			DeactivateTowerCamera();
		}

		if (UIMisc.bIsGameOver) {
			cMainCamera.GetComponent<TestCamera> ().bCanMoveCamera = false;
		}

	}

	void OnMouseEnter() {
		goHighlightedTower.SetActive (true);
	}
	
	void OnMouseExit() {
		goHighlightedTower.SetActive (false);
	}

	void OnMouseDown(){
		if (!EventSystem.current.IsPointerOverGameObject()) {
			if (!bInTower) {
				ActivateTowerCamera();
			}
		}
	}

	void ActivateTowerCamera(){
		cMainCamera.GetComponent<TestCamera> ().bCanMoveCamera = false;
		cTowerCamera.gameObject.SetActive(true);
		canMainCanvas.GetComponent<Canvas>().enabled = false;
		AudioSource.PlayClipAtPoint(audioManager.acEnterTower, Camera.main.transform.position);
		Screen.lockCursor = true;
		bInTower = true;
	}

	void DeactivateTowerCamera() {
		cMainCamera.GetComponent<TestCamera> ().bCanMoveCamera = true;
		cTowerCamera.gameObject.SetActive(false);
		canMainCanvas.GetComponent<Canvas>().enabled = true;
		AudioSource.PlayClipAtPoint(audioManager.acLeaveTower, Camera.main.transform.position);
		Screen.lockCursor = false;
		bInTower = false;
	}
}

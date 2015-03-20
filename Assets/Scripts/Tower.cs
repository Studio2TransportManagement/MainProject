using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

	private bool bIsEmpty;
	public GameObject goStationedUnit;
	public Camera cTowerCamera;
	public Camera cMainCamera;


	//
	void Start(){
		cMainCamera.enabled = true;
		cTowerCamera.enabled = false;
		//bIsEmpty = true;
	}

	// Update is called once per frame
	void Update () {

		if(bIsEmpty == false && Input.GetKeyDown(KeyCode.C))
		{
			SwitchCamera();
		}

	
	}

	public void SwitchCamera(){
		cTowerCamera.enabled = !cTowerCamera.enabled;
		cMainCamera.enabled = !cTowerCamera.enabled;
		Screen.showCursor = !Screen.showCursor;
	}

	public void FillTower(){
		bIsEmpty = false;
	}

	public void EmptyTower(){
		bIsEmpty = true;
		//cTowerCamera.camera.enabled = false;
	}
}

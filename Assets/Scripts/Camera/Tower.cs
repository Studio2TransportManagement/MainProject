using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

	public Camera cTowerCamera;
	public Camera cMainCamera;
	public Canvas canMainCanvas;


	//
	void Start(){
		cTowerCamera.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(1) && cTowerCamera.gameObject.activeInHierarchy == true)
		{
			SwapCamera();
		}
	}

	void OnMouseDown(){
		Debug.Log("Clicked");
		SwapCamera();
	}

	void SwapCamera(){
		Debug.Log("Swapping");
		cMainCamera.GetComponent<TestCamera> ().bCanMoveCamera = !cMainCamera.GetComponent<TestCamera> ().bCanMoveCamera;
		cTowerCamera.gameObject.SetActive(!cTowerCamera.gameObject.activeInHierarchy);
		canMainCanvas.gameObject.SetActive (!canMainCanvas.gameObject.activeInHierarchy);
		Screen.lockCursor = !Screen.lockCursor;
	}
}

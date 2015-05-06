using UnityEngine;
using System.Collections;

public class TestCamera : MonoBehaviour {

	public float fCamPanSpeed;
	public float fCamZoomSpeed;

	public Vector3 vRot;

	public float fCamZoomInLimit;
	public float fCamZoomOutLimit;
	public bool bCanMoveCamera = true;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(bCanMoveCamera)
		{
			if(Input.GetAxis("Horizontal")< 0)
			{
				this.transform.RotateAround(vRot, Vector3.up, -fCamPanSpeed*Time.deltaTime);
			}
			
			if(Input.GetAxis("Horizontal")> 0)
			{
				this.transform.RotateAround(vRot, Vector3.up, fCamPanSpeed*Time.deltaTime);
			}
		}

//		if (Input.GetAxis("Mouse ScrollWheel") < 0 ) 
//		{
//			transform.Translate(-transform.forward);
//		}
//		
//		if (Input.GetAxis("Mouse ScrollWheel") > 0) 
//		{
//			transform.Translate(transform.forward);
//		}

	}
}

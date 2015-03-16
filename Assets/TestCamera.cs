using UnityEngine;
using System.Collections;

public class TestCamera : MonoBehaviour {

	public float fCamPanSpeed;
	public float fCamZoomSpeed;

	public float fCamZoomInLimit;
	public float fCamZoomOutLimit;

	public GameObject goTargetObj;

	public bool target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

//		if(!target)
//		{
//			this.transform.LookAt(goTargetObj.transform);
//		}



		if(Input.GetKey(KeyCode.A))
		{
			this.transform.RotateAround(Vector3.zero, Vector3.up, -fCamPanSpeed*Time.deltaTime);
		}

		if(Input.GetKey(KeyCode.D))
		{
			this.transform.RotateAround(Vector3.zero, Vector3.up, fCamPanSpeed*Time.deltaTime);
		}

		if (Input.GetAxis("Mouse ScrollWheel") < 0 && this.transform.position.y < fCamZoomOutLimit) 
		{
			this.transform.position += new Vector3(0.0f, fCamZoomSpeed * Time.deltaTime, 0.0f);
		}
		
		if (Input.GetAxis("Mouse ScrollWheel") > 0 && this.transform.position.y > fCamZoomInLimit) 
		{
			this.transform.position -= new Vector3(0.0f, fCamZoomSpeed * Time.deltaTime, 0.0f);
		}

	}
}

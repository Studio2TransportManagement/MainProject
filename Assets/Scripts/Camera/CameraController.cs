using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float fCamPanSpeed = 0.16f;
	public float fCamSweepSpeed = 0.32f;

	public float fCamLeftLimit = -5.0f;
	public float fCamRightLimit = 5.0f;
	public float fCamFrontLimit = 8.0f;
	public float fCamBackLimit = -8.0f;
	public float fCamUpLimit = 7.0f;
	public float fCamDownLimit = 1.0f;

	//public float fCamSweepX = 44.0f;
	//public float fCamSweepXSpeed = 0.5f;
	public float fCamSweepXMax = 44.0f;
	public float fCamSweepXMin = 5.0f;
	public GameObject goCamTarget;

	//Init
	void Start () {
		//
	}
	
	//Update
	void Update () {
		//Panning
		if (Input.GetKey(KeyCode.A)) {
			this.transform.position = this.transform.position - this.transform.right * fCamPanSpeed;
		}
		else if (Input.GetKey(KeyCode.D)) {
			this.transform.position = this.transform.position + this.transform.right * fCamPanSpeed;
		}

		if (Input.GetKey(KeyCode.W)) {
			this.transform.position = this.transform.position + this.transform.forward * fCamPanSpeed;
		}
		else if (Input.GetKey(KeyCode.S)) {
			this.transform.position = this.transform.position - this.transform.forward * fCamPanSpeed;
		}

		//Pan Limit
		if (this.transform.position.x > fCamRightLimit) {
			this.transform.position = new Vector3(fCamRightLimit, this.transform.position.y, this.transform.position.z);
		}
		if (this.transform.position.x < fCamLeftLimit) {
			this.transform.position = new Vector3(fCamLeftLimit, this.transform.position.y, this.transform.position.z);
		}

		if (this.transform.position.z > fCamFrontLimit) {
			this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, fCamFrontLimit);
		}
		if (this.transform.position.z < fCamBackLimit) {
			this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, fCamBackLimit);
		}

		//Sweep
		if (Input.GetAxis("Mouse ScrollWheel") < 0 && this.transform.position.y < fCamUpLimit) {
			this.transform.position = this.transform.position + new Vector3(0.0f, fCamSweepSpeed, 0.0f);
		}

		if (Input.GetAxis("Mouse ScrollWheel") > 0 && this.transform.position.y > fCamDownLimit) {
			this.transform.position = this.transform.position - new Vector3(0.0f, fCamSweepSpeed, 0.0f);
		}

		//Sweep Limit
		if (this.transform.position.y > fCamUpLimit) {
			this.transform.position = new Vector3(this.transform.position.x, fCamUpLimit, this.transform.position.z);
		}
		if (this.transform.position.y < fCamDownLimit) {
			this.transform.position = new Vector3(this.transform.position.x, fCamDownLimit, this.transform.position.z);
		}

		//Reset
		if (Input.GetMouseButtonDown(2)) {
			this.transform.position = new Vector3(0.0f, fCamUpLimit, fCamBackLimit);
			this.transform.eulerAngles = new Vector3(fCamSweepXMax, 0.0f, 0.0f);
		}

		//Look
		this.transform.LookAt(goCamTarget.transform);
	}
}

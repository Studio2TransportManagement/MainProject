using UnityEngine;
using System.Collections;

public class SlideToLocation : MonoBehaviour {

	public Vector3 vTarget;
	public float fSpeed = 0.08f;

	// Use this for initialization
	void Start () {
	
	}

	bool PosApprox(Vector3 a, Vector3 b) {
		if (Mathf.Approximately (a.x, b.x)) {
			if (Mathf.Approximately (a.y, b.y)) {
				if (Mathf.Approximately (a.z, b.z)) {
					return true;
				}
			}
		}

		return false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!PosApprox(this.transform.position, vTarget)) {
			if (this.transform.position.x < vTarget.x) {
				this.transform.position = new Vector3(this.transform.position.x + fSpeed, this.transform.position.y, this.transform.position.z);
			}

			if (this.transform.position.x > vTarget.x) {
				this.transform.position = new Vector3(this.transform.position.x - fSpeed, this.transform.position.y, this.transform.position.z);
			}

			if (this.transform.position.z < vTarget.z) {
				this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + fSpeed);
			}
			
			if (this.transform.position.z > vTarget.z) {
				this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - fSpeed);
			}
		}
	}
}

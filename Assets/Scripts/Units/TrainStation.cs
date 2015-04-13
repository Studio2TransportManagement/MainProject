using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrainStation : MonoBehaviour {

	public BoxCollider boxcolPickupPoint;

	public List<GameObject> l_goWaiting;

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "player-unit") {
			l_goWaiting.Add(other.gameObject);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "player-unit") {
			if (l_goWaiting.Contains(other.gameObject)) {
				l_goWaiting.Remove(other.gameObject);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

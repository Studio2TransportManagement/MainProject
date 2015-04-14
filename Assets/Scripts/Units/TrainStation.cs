using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrainStation : MonoBehaviour {

	public List<GameObject> l_goWaiting;

	private Train trTrain;
	public Train trProtoTrain;

	public Transform tStartPoint;
	public Transform tEndPoint;

	private SlideToLocation stlTrainSlide;
	// Use this for initialization
	void Start() {
				
		trTrain = (Train)Instantiate(trProtoTrain, tStartPoint.transform.position, tStartPoint.rotation);
		trTrain.transform.localScale = tStartPoint.localScale;
		stlTrainSlide = trTrain.GetComponent<SlideToLocation>();
	}
	
	// Update is called once per frame
	void Update() {
		stlTrainSlide.vTarget = tStartPoint.position;
	}


	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "player-unit" && other.GetComponent<PlayerUnit>().bInTransit == true) {
			l_goWaiting.Add(other.gameObject);
			other.gameObject.SetActive (false);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "player-unit") {
			if (l_goWaiting.Contains(other.gameObject)) {
				l_goWaiting.Remove(other.gameObject);
			}
		}
	}

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrainTrack : MonoBehaviour {

	public List<TrainStation> l_tsNodes;
	public Train guTrain;

	// Use this for initialization
	void Start() {
		TrainStation[] nodes = this.GetComponentsInChildren<TrainStation>();
		l_tsNodes.AddRange(nodes);
		guTrain = this.GetComponentInChildren<Train>();
	}
	
	// Update is called once per frame
	void Update() {
		guTrain.GetComponent<SlideToLocation>().vTarget = l_tsNodes[0].transform.position;


		//Temp
		if (Input.GetKeyUp(KeyCode.T)) {
			l_tsNodes.Reverse();
		}
	}
}

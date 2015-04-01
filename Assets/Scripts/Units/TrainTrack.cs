using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrainTrack : MonoBehaviour {

	private List<TrainStation> l_tsNodes;
	private Train guTrain;
	public GameUnit guProtoTrain;

	// Use this for initialization
	void Start() {
		l_tsNodes = new List<TrainStation>();
		TrainStation[] nodes = this.GetComponentsInChildren<TrainStation>();
		l_tsNodes.AddRange(nodes);

		guTrain = (Train)Instantiate(guProtoTrain, l_tsNodes[0].transform.position, this.transform.rotation);

	}
	
	// Update is called once per frame
	void Update() {
		guTrain.GetComponent<SlideToLocation>().vTarget = l_tsNodes[0].transform.position;

		if (Input.GetMouseButtonUp(1)) {
			if (guTrain.IsSelected()) {
				FlipDestination();
			}
		}
	}

	void FlipDestination() {
		l_tsNodes.Reverse();
	}
}

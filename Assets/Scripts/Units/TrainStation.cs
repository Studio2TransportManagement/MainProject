using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrainStation : MonoBehaviour {

	public List<GameObject> l_goWaiting;

	private Train trTrain;
	public Train trProtoTrain;

	public Transform tStartPoint;
	public Transform tEndPoint;
	public Transform vOffStation;

	public float fLerpTimer = 0f;

	private SlideToLocation stlTrainSlide;
	public float fCountdown = 5.0f;
	public int sCapacity = 4;
	public bool bTravelling = false;
	
	// Use this for initialization
	void Start() {
		trTrain = (Train)Instantiate(trProtoTrain, tStartPoint.transform.position, tStartPoint.rotation);
		trTrain.transform.localScale = tStartPoint.localScale;
		//stlTrainSlide = trTrain.GetComponent<SlideToLocation>();
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		//stlTrainSlide.vTarget = tStartPoint.position;
		
		if (!bTravelling)
		{
			if (l_goWaiting.Count > 0) {
				if (fCountdown == 0f) {
					fCountdown = 5f;
				}
				else
				{
					fCountdown -= Time.fixedDeltaTime;
				}
			}
			if (l_goWaiting.Count == sCapacity || fCountdown <= 0.0f)
			{
				bTravelling = true;
			}
		}
		 if (bTravelling)
		{
				trTrain.transform.position = Vector3.Lerp( trTrain.transform.position, tEndPoint.position, fLerpTimer/5f);
				fLerpTimer += Time.fixedDeltaTime;
				if (fLerpTimer <= 5.0f)
				{
					foreach(GameObject obj in l_goWaiting)
					{
							obj.transform.position  = new Vector3 (Random.insideUnitCircle.x + vOffStation.position.x, vOffStation.position.y, Random.insideUnitCircle.y + vOffStation.position.z);
							obj.SetActive(true);
							obj.GetComponent<PlayerUnit>().ChangeState(new StateSoldierIdle());
							
					}
					l_goWaiting.Clear();
				}
		}
		else if (fLerpTimer == 5.0f)
		{
		fLerpTimer = 0.0f;
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "player-unit" && other.gameObject.GetComponent<PlayerUnit>().bInTransit == true) {
			Debug.Log ("Did collide");
			l_goWaiting.Add(other.gameObject);
			other.gameObject.SetActive (false);
		}
		else{
			Debug.Log ("Didn't Collide");
		}
	}

//	void OnCollisionEnter(Collision other) {
//		if (other.gameObject.tag == "player-unit" && other.gameObject.GetComponent<PlayerUnit>().bInTransit == true) {
//			Debug.Log ("Did collide");
//			l_goWaiting.Add(other.gameObject);
//			other.gameObject.SetActive (false);
//		}
//		else{
//			Debug.Log ("Didn't Collide");
//		}
//	}

//	void OnCollisionExit(Collision other) {
//		if (other.gameObject.tag == "player-unit") {
//			if (l_goWaiting.Contains(other.gameObject)) {
//				l_goWaiting.Remove(other.gameObject);
//			}
//		}
//	}

}

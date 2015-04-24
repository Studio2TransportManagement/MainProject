using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrainStation : MonoBehaviour {

	public List<GameObject> l_goWaiting;
	public List<GameObject> l_goOnTrain;

	private Train trTrain;
	public Train trProtoTrain;

	public Transform tStartPoint;
	public Transform tEndPoint;
	public Transform vOffStation;

	public float fLerpTimer = 0.0f;
	
	public float fCountdown = 5.0f;
	public int iCapacity = 4;
	public bool bTravelling = false;
	public bool bReversing = false;

	public BaseGameStructure bsDestinationBase;
	
	// Use this for initialization
	void Start() {
		trTrain = (Train)Instantiate(trProtoTrain, tStartPoint.transform.position, tStartPoint.rotation);
		trTrain.transform.localScale = tStartPoint.localScale;

		l_goWaiting = new List<GameObject>();
		l_goOnTrain = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update() {
		//Waiting for motion
		if (!bTravelling) {
			if (l_goWaiting.Count > 0) {
				//Countdown to depature
				fCountdown -= Time.deltaTime;
			}

			//Leave if full or timed out
			if (l_goWaiting.Count >= iCapacity || fCountdown <= 0.0f) {
				bTravelling = true;
				fCountdown = 5.0f;
				fLerpTimer = 0.0f;

				//Move troops onto train
				if (l_goWaiting.Count >= iCapacity) {
					l_goOnTrain = l_goWaiting.GetRange(0, iCapacity);
					l_goWaiting.RemoveRange(0, iCapacity);
				}
				else {
					l_goOnTrain.AddRange(l_goWaiting.GetRange(0, l_goWaiting.Count));
					l_goWaiting.RemoveRange(0, l_goWaiting.Count);
				}
			}
		}

		if (bTravelling) {
			if (!bReversing) {
				//We're leaving, lerp there nicely
				trTrain.transform.position = Vector3.Lerp(trTrain.transform.position,
				                                          tEndPoint.position,
				                                          fLerpTimer / 2.0f);
				fLerpTimer += Time.deltaTime;

				//Finished lerp
				if (fLerpTimer >= 2.0f) {
					foreach (GameObject obj in l_goOnTrain) {
						//Position units
						obj.GetComponent<PlayerUnit>().bInTransit = false;
						obj.transform.position  = new Vector3 (Random.insideUnitCircle.x + vOffStation.position.x,
						                                       vOffStation.position.y,
						                                       Random.insideUnitCircle.y + vOffStation.position.z);
						obj.SetActive(true);
						obj.GetComponent<PlayerUnit>().ChangeState(new StateSoldierIdle());
					}
					fLerpTimer = 0.0f;
					bReversing = true;
					l_goOnTrain.Clear();
				}
			}
			else {
				//Reverse back to our loading platform
				trTrain.transform.position = Vector3.Lerp(trTrain.transform.position,
				                                          tStartPoint.position,
				                                          fLerpTimer / 2.0f);
				fLerpTimer += Time.deltaTime;

				if (fLerpTimer >= 2.0f) {
					bReversing = false;
					bTravelling = false;
				}
			}
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "player-unit" && other.gameObject.GetComponent<PlayerUnit>().bInTransit == true) {
			//Debug.Log ("Did collide");
			l_goWaiting.Add(other.gameObject);
			other.gameObject.SetActive(false);
		}
		else {
			//Debug.Log ("Didn't Collide");
		}
	}

	public BaseGameStructure GetDestinationBase() {
		if (bsDestinationBase == null) {
			Debug.Log("<color=red>TrainStation: bsDestinationBase was NULL!</color>");
		}
		return bsDestinationBase;
	}

}

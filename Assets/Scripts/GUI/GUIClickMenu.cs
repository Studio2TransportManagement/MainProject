using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class GUIClickMenu : MonoBehaviour {

	public GameObject goUnit;
	public GameObject goSpawner;

	//Default choice for the enumerator. 
	public SOLDIER_TYPE troopChoice = SOLDIER_TYPE.GUNNER;

	public bool bMenuOn = false;

	void Start() {
		this.GetComponent<Button>().onClick.AddListener(() => { 
			SpawnSoldier(troopChoice); 
		});
	}

	void Update() {
		this.GetComponent<Button>().enabled = bMenuOn;
		this.GetComponent<Button>().image.enabled = bMenuOn;
		this.GetComponentInChildren<Text>().enabled = bMenuOn;
	}

	public void ToggleVisibility() {
		bMenuOn = !bMenuOn;
	}

    void SpawnSoldier(SOLDIER_TYPE sol) {
		if (sol == SOLDIER_TYPE.GUNNER) {
			Debug.Log("GUNNER");
			GameObject lastSpawned = (GameObject)Instantiate(goUnit, goSpawner.transform.position + new Vector3(0.0f, 0.15f, 0.0f), Quaternion.identity);
			lastSpawned.GetComponent<SlideToLocation>().vTarget.x += Random.Range(-0.5f, 0.5f);
		}

		if (sol == SOLDIER_TYPE.HEAVY) {
			Debug.Log("HEAVY");

		}
		if (sol == SOLDIER_TYPE.MEDIC) {
			Debug.Log("MEDIC");
		}
		if (sol == SOLDIER_TYPE.MECHANIC) {
			Debug.Log("MECHANIC");
		}
	}
}
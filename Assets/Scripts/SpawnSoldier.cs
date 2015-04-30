using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class SpawnSoldier : MonoBehaviour {

	public GameObject goProtoUnit;
	private PlayerResources pPlayerResources;

	void Awake () {
		pPlayerResources = FindObjectOfType<PlayerResources>();
	}

	void Start() {
		//
	}

	void Update() {
		//
	}

    public void SpawnUnit(SOLDIER_TYPE sol, Vector3 spawnpoint, Vector2 variance, string name) {
		GameObject lastSpawned = null;

		spawnpoint = spawnpoint + new Vector3(variance.x, 0.0f, variance.y);

		if (sol == SOLDIER_TYPE.GUNNER) {
			lastSpawned = (GameObject)Instantiate(goProtoUnit, spawnpoint, Quaternion.identity);
			pPlayerResources.iTotalRecruits ++;
		}

		if (sol == SOLDIER_TYPE.HEAVY) {
			lastSpawned = (GameObject)Instantiate(goProtoUnit, spawnpoint, Quaternion.identity);
			pPlayerResources.iTotalRecruits ++;
		}
		if (sol == SOLDIER_TYPE.MEDIC) {
			lastSpawned = (GameObject)Instantiate(goProtoUnit, spawnpoint, Quaternion.identity);
			pPlayerResources.iTotalRecruits ++;
		}
		if (sol == SOLDIER_TYPE.MECHANIC) {
			lastSpawned = (GameObject)Instantiate(goProtoUnit, spawnpoint, Quaternion.identity);
			pPlayerResources.iTotalRecruits ++;
		}

		lastSpawned.GetComponent<PlayerUnit>().sUnitName = name;

		lastSpawned.GetComponent<NavMeshAgent>().SetDestination(spawnpoint + new Vector3(0,0,-5));
	}
}
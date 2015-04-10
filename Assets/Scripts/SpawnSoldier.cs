using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class SpawnSoldier : MonoBehaviour {

	public GameObject goProtoUnit;
	private string sName;

	void Start() {
		//
	}

	void Update() {
		//
	}

    public void SpawnUnit(SOLDIER_TYPE sol, Vector3 spawnpoint, string name) {
		if (sol == SOLDIER_TYPE.GUNNER) {
			GameObject lastSpawned = (GameObject)Instantiate(goProtoUnit, spawnpoint, Quaternion.identity);
			lastSpawned.GetComponent<PlayerUnit>().sUnitName = name;
			lastSpawned.GetComponent<NavMeshAgent>().SetDestination(spawnpoint + new Vector3(0,0,-5)); 
		}

		if (sol == SOLDIER_TYPE.HEAVY) {
			GameObject lastSpawned = (GameObject)Instantiate(goProtoUnit, spawnpoint, Quaternion.identity);
			lastSpawned.GetComponent<PlayerUnit>().sUnitName = name;
			lastSpawned.GetComponent<NavMeshAgent>().SetDestination(spawnpoint + new Vector3(0,0,-5));

		}
		if (sol == SOLDIER_TYPE.MEDIC) {
			GameObject lastSpawned = (GameObject)Instantiate(goProtoUnit, spawnpoint, Quaternion.identity);
			lastSpawned.GetComponent<PlayerUnit>().sUnitName = name;
			lastSpawned.GetComponent<NavMeshAgent>().SetDestination(spawnpoint + new Vector3(0,0,-5));
		}
		if (sol == SOLDIER_TYPE.MECHANIC) {
			GameObject lastSpawned = (GameObject)Instantiate(goProtoUnit, spawnpoint, Quaternion.identity);
			lastSpawned.GetComponent<PlayerUnit>().sUnitName = name;
			lastSpawned.GetComponent<NavMeshAgent>().SetDestination(spawnpoint + new Vector3(0,0,-5));
		}
	}
}
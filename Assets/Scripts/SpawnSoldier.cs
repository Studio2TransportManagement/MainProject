﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class SpawnSoldier : MonoBehaviour {

	public GameObject goProtoUnit;
	//private Vector3 vSpawnPoint;
	private string sName;

	//Default choice for the enumerator. 
	//private SOLDIER_TYPE troopChoice = SOLDIER_TYPE.GUNNER;

	void Start() {
//		this.GetComponent<Button>().onClick.AddListener(() => { 
//			SpawnSoldier(troopChoice); 
//		});
	}

	void Update() {
		//
	}

    public void SpawnUnit(SOLDIER_TYPE sol, Vector3 spawnpoint, string name) {
		if (sol == SOLDIER_TYPE.GUNNER) {
			GameObject lastSpawned = (GameObject)Instantiate(goProtoUnit, spawnpoint, Quaternion.identity);
			lastSpawned.GetComponent<GameUnit>().sUnitName = name;
			//Fluff
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
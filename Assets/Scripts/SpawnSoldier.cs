using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class SpawnSoldier : MonoBehaviour {

	public GameObject goProtoUnit;

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
		}

		if (sol == SOLDIER_TYPE.HEAVY) {
			lastSpawned = (GameObject)Instantiate(goProtoUnit, spawnpoint, Quaternion.identity);
		}
		if (sol == SOLDIER_TYPE.MEDIC) {
			lastSpawned = (GameObject)Instantiate(goProtoUnit, spawnpoint, Quaternion.identity);
		}
		if (sol == SOLDIER_TYPE.MECHANIC) {
			lastSpawned = (GameObject)Instantiate(goProtoUnit, spawnpoint, Quaternion.identity);
		}

		//Easter eggs are small and numerous
		if (sol == SOLDIER_TYPE.GUNNER && name == "Shia LeBeouf") {
			PlayerUnit shia = lastSpawned.GetComponent<Gunner>();
			shia.sUnitName = name;
			shia.SollyType = SOLDIER_TYPE.VILLAGER;
			shia.fDamage = 10.0f;
			shia.fFireRate = 0.01f;
			shia.fHealthMax = 500.0f;
			shia.fHealthCurrent = 500.0f;
			shia.fReloadSpeed = 1.0f;
			shia.iMaxAmmo = 200;
			shia.transform.localScale *= 2;

			UnityEditor.Selection.activeGameObject = shia.gameObject;
		}
		else {
			lastSpawned.GetComponent<PlayerUnit>().sUnitName = name;
		}

		lastSpawned.GetComponent<NavMeshAgent>().SetDestination(spawnpoint + new Vector3(0,0,-5));
	}
}
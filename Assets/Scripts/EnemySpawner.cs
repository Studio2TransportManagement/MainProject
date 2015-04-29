﻿using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject goEnemyGunner;
	public GameObject goEnemyTank;
	
	public GameObject[] goSpawnPoints;

	//The Size of the first wave
	public int iStartingWaveSize;
	//Counting down how many Gunners are left to spawn in the current wave
	public int iCurrentWaveCount;
	//How many units there are in total in the current wave
	public int iCurrentWaveSize;
	
	public float fSpawnRate;
	public float fWaveRate;
	private float fSpawnRadius;

	public float fTankSpawnRate;
	public float fTankSpawnCounter;

	private bool bSpawnCoroutineRunning;

	
	void Start () 
	{
		bSpawnCoroutineRunning = false;
		
		//By extracting the bounds from each SpawnPoint object, we don't need to have gameobject.collider.bounds.FUNCTION later when spawning, also allows to easily change from coolider to other.boundsp
//		bSpawnBounds = BuildBoundsArray(goSpawnPoints);
		fSpawnRadius = goSpawnPoints[1].GetComponent<SphereCollider>().radius;
		iCurrentWaveSize = iStartingWaveSize;
		StartSpawning();

	}
	
	
	void Update () 
	{
		
	}
	
	
	IEnumerator SpawnEnemies(){
		bSpawnCoroutineRunning = true;
		//replace true with GameOver=False or something later
		while(true)
		{
			iCurrentWaveCount = iCurrentWaveSize;
			float[] WaveSizes = CalculateWaveSizes(GenerateRatioArray());

			if(iCurrentWaveSize < 9 ){
				while(iCurrentWaveCount > 0) {

					for(int i = 0; i < goSpawnPoints.Length; i++){
						if(iCurrentWaveCount > 0) { 
							Vector3 spawnPos = new Vector3 (
								(Random.insideUnitCircle*fSpawnRadius).x +goSpawnPoints[i].transform.position.x,
								goSpawnPoints[i].transform.position.y,
								(Random.insideUnitCircle*fSpawnRadius).y +goSpawnPoints[i].transform.position.z
								);
							Instantiate(goEnemyGunner,spawnPos,Quaternion.identity);
							iCurrentWaveCount--;
						}
						else {
							break;
						}
					}

					yield return new WaitForSeconds (fSpawnRate);
				}
				iCurrentWaveSize++;
				yield return new WaitForSeconds (fWaveRate);
			}

			//once Initial waves have been expended, start spawning larger waves
			if(iCurrentWaveSize >= 9) {
				while(iCurrentWaveCount > 0)
				{
					for(int i = 0; i < WaveSizes.Length; i++)
					{
						for(int a = (int)WaveSizes[i]; a > 0 ; a--)
						{
							Vector3 spawnPos = new Vector3 (
								(Random.insideUnitCircle*fSpawnRadius).x +goSpawnPoints[i].transform.position.x,
								goSpawnPoints[i].transform.position.y,
								(Random.insideUnitCircle*fSpawnRadius).y +goSpawnPoints[i].transform.position.z
							);

							Instantiate(goEnemyGunner,spawnPos,Quaternion.identity);
							iCurrentWaveCount--;
							
							if(fTankSpawnCounter >= fTankSpawnRate) {
								spawnPos = new Vector3 (
									(Random.insideUnitCircle*fSpawnRadius).x +goSpawnPoints[i].transform.position.x,
									goSpawnPoints[i].transform.position.y,
									(Random.insideUnitCircle*fSpawnRadius).y +goSpawnPoints[i].transform.position.z
								);
								Instantiate(goEnemyTank ,spawnPos, Quaternion.identity);	
								fTankSpawnCounter = 0f;
							}
							fTankSpawnCounter += Random.Range(0f,1f);
						}
					}
					
					yield return new WaitForSeconds (fSpawnRate);
				}
				iCurrentWaveSize += 2;
				yield return new WaitForSeconds (fWaveRate);
			}

		}
	}

	//divide the total size of the wave into the generated ratio and store each 
	float[] CalculateWaveSizes(int[] RatioArray)
	{
		float[] iDividedWaves = new float[RatioArray.Length];
		float iPartValue = 0;
		float iRatioTotal = 0;
		
		foreach(int value in RatioArray)
		{
			iRatioTotal += (float)value;
		}
		
		iPartValue = iCurrentWaveSize/iRatioTotal;
		
		for(int i = 0; i<iDividedWaves.Length; i++)
		{
			iDividedWaves[i] = Mathf.Round((float)RatioArray[i]*iPartValue);
		}
		
		//		Debug.Log (string.Format("Wave Sizes {0}:{1}:{2}", iDividedWaves[0],iDividedWaves[1],iDividedWaves[2]));
		
		return iDividedWaves;
	}
	
	//generate a random set of numbers for use in a ratio array equal to how many spawn points there are
	int[] GenerateRatioArray()
	{
		int[] RatioArray = new int[goSpawnPoints.Length];
		int iGCD;
		
		for(int i=0; i<RatioArray.Length; i++)
		{
			RatioArray[i] = Random.Range(1,11);
		}
		
		//find the GCD of the random ratio
		iGCD = FindGCD(FindGCD (RatioArray[0],RatioArray[1]), RatioArray[2]);
		
		for(int i=0; i<RatioArray.Length; i++)
		{
			RatioArray[i] /= iGCD;
		}
		
		//		Debug.Log (string.Format("Ratio {0}:{1}:{2}", RatioArray[0],RatioArray[1],RatioArray[2]));
		
		
		return RatioArray;
		
	}
	
	
	
	//find the Greatest Common Denominator of two numbers
	int FindGCD(int a, int b)
	{
		return b == 0 ? a : FindGCD(b, a % b);
	}

	public bool StartSpawning(){
		if(!bSpawnCoroutineRunning){
			StartCoroutine(SpawnEnemies());
			return true;
		}
		else{
			return false;
		}
	}
}

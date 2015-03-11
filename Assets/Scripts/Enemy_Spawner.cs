using UnityEngine;
using System.Collections;

public class Enemy_Spawner : MonoBehaviour {

	public GameObject[] goEnemies;

	public GameObject[] goSpawnPoints;

	public int iEnemyWaveSize;

	public int i;
	
	public float fSpawnRate;

	Bounds[] bSpawnBounds;


	void Start () {

		//By extracting the bounds from each SpawnPoint object, we don't need to have gameobject.collider.bounds.FUNCTION later when spawning, also allows to easily change from coolider to other.boundsp
		bSpawnBounds = BuildBoundsArray(goSpawnPoints);
		foreach(Bounds element in bSpawnBounds)
		{
			Debug.Log(element.ToString());
		}
		GenerateRatioArray();
		StartCoroutine (SpawnEnemies());
	}
	

	void Update () 
	{
	
	}


	IEnumerator SpawnEnemies()
	{
		int CurrentWaveMax = iEnemyWaveSize;
		//replace true with GameOver=False or something later
		while(true)
		{
			if(CurrentWaveMax >=0)
			{
				Vector3 spawnPos = new Vector3 (Random.Range(bSpawnBounds[i].max.x,bSpawnBounds[i].min.x),1,(Random.Range(bSpawnBounds[i].max.z,bSpawnBounds[i].min.z)));
				Instantiate(goEnemies[Random.Range(0,goEnemies.Length)],spawnPos,Quaternion.identity);
				CurrentWaveMax--;
				yield return new WaitForSeconds (fSpawnRate);
			}

		}
	}


	//create an array of bounds for each GameObject's bounds
	Bounds[] BuildBoundsArray(GameObject[] goObjects)
	{
		Bounds[] bOutputBounds = new Bounds[goObjects.Length];

		for(int i = 0; i < goObjects.Length; i++)
		{
			bOutputBounds[i] = goObjects[i].collider.bounds;
		}

		return bOutputBounds;
	}

	//divide the total size of the wave into the generated ratio and store each 
	int[] CalculateWaveSizes(int[] RatioArray)
	{
		int[] iDividedWaves = new int[RatioArray.Length];
		int iPartValue = 0;
		int iRatioTotal = 0;

		foreach(int value in RatioArray)
		{
			iRatioTotal += value;
		}

		iPartValue = iEnemyWaveSize/iRatioTotal;

		for(int i = 0; i<iDividedWaves.Length; i++)
		{
			iDividedWaves[i] = RatioArray[i]*iPartValue;
		}

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

		Debug.Log (string.Format("{0}:{1}:{2}", RatioArray[0],RatioArray[1],RatioArray[2]));


		return RatioArray;

	}



	//find the Greatest Common Denominator of two numbers
	int FindGCD(int a, int b)
	{
		return b == 0 ? a : FindGCD(b, a % b);
	}


}

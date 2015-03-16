using UnityEngine;
using System.Collections;

public class Enemy_Spawner : MonoBehaviour {

	public GameObject[] goEnemies;

	public GameObject[] goSpawnPoints;

	public int iEnemyWaveSize;
	public int CurrentWaveCount;
	
	public float fSpawnRate;
	public float fWaveRate;

	Bounds[] bSpawnBounds;


	void Start () 
	{
		
		//By extracting the bounds from each SpawnPoint object, we don't need to have gameobject.collider.bounds.FUNCTION later when spawning, also allows to easily change from coolider to other.boundsp
		bSpawnBounds = BuildBoundsArray(goSpawnPoints);

		StartCoroutine (SpawnEnemies());
	}
	

	void Update () 
	{
	
	}


	IEnumerator SpawnEnemies()
	{

		//replace true with GameOver=False or something later
		while(true)
		{
			CurrentWaveCount = iEnemyWaveSize;
			float[] WaveSizes = CalculateWaveSizes(GenerateRatioArray());


			while(CurrentWaveCount > 0)
			{
				for(int i = 0; i < WaveSizes.Length; i++)
				{
					for(int a = (int)WaveSizes[i]; a > 0 ; a--)
					{
						Vector3 spawnPos = new Vector3 (
							Random.Range(bSpawnBounds[i].max.x,bSpawnBounds[i].min.x),
							1,
							Random.Range(bSpawnBounds[i].max.z,bSpawnBounds[i].min.z));

						Instantiate(goEnemies[Random.Range(0,goEnemies.Length)],spawnPos,Quaternion.identity);
						CurrentWaveCount--;
					}
				}
								
				yield return new WaitForSeconds (fSpawnRate);
			}

			yield return new WaitForSeconds (fWaveRate);
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
	float[] CalculateWaveSizes(int[] RatioArray)
	{
		float[] iDividedWaves = new float[RatioArray.Length];
		float iPartValue = 0;
		float iRatioTotal = 0;

		foreach(int value in RatioArray)
		{
			iRatioTotal += (float)value;
		}

		iPartValue = iEnemyWaveSize/iRatioTotal;

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


}

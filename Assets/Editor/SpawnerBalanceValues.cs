using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SpawnerBalanceValues {
	
	public Dictionary<string, float> d_sfToBeAppliedValues;
	
	public EnemySpawner esSpawner;
	
	public bool bValuesTabOpen;
	
	public SpawnerBalanceValues(EnemySpawner spawner) {
		esSpawner = spawner;

		d_sfToBeAppliedValues = new Dictionary<string, float>();

		bValuesTabOpen = false;
		d_sfToBeAppliedValues.Add("Starting Wave Size", (float)esSpawner.iStartingWaveSize);
		d_sfToBeAppliedValues.Add("Added each wave", (float)esSpawner.iWaveIncrease);
		d_sfToBeAppliedValues.Add("Wave Timer", esSpawner.fWaveRate);
		d_sfToBeAppliedValues.Add("Ratio Split wave", (float)esSpawner.iRatioSplitWave);
		d_sfToBeAppliedValues.Add("Tank Spawn Factor", esSpawner.fTankSpawnRate);

	}

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class FortBalanceValues {
		
	public Dictionary<string, float> d_sfToBeAppliedValues;
	
	public BaseGameStructure bgsBase;
	
	public bool bValuesTabOpen;
	
	public FortBalanceValues(BaseGameStructure fort) {
		bgsBase = fort;
		d_sfToBeAppliedValues = new Dictionary<string, float>();

		bValuesTabOpen = false;

		d_sfToBeAppliedValues.Add("Health", bgsBase.fHealthMax);
		d_sfToBeAppliedValues.Add("Health Upg. Cost", (float)bgsBase.iIntegrityUpgradeCost);
		d_sfToBeAppliedValues.Add("Health Upg. Amount", (float)bgsBase.iIntegrityUpgradeAmount);
		d_sfToBeAppliedValues.Add("Window Upg. Cost", (float)bgsBase.iWindowUpgradeCost);
		d_sfToBeAppliedValues.Add("Trains Upg. Cost", (float)bgsBase.iTrainsUpgradeCost);

	}

}

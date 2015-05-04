using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameUnitBalance{

	public Dictionary<GUIContent, float> d_sfToBeAppliedValues;

	public GameUnit guGameUnit;

	public bool bValuesTabOpen;

	public float[] fVsTargets = {0f,0f,0f};

	public GameUnitBalance(GameUnit gu, Dictionary<string, GUIContent> dic) {

		d_sfToBeAppliedValues = new Dictionary<GUIContent, float>();

		guGameUnit = gu;

		bValuesTabOpen = false;

		d_sfToBeAppliedValues.Add(dic["Health"], gu.fHealthMax);
		d_sfToBeAppliedValues.Add(dic["Damage"], gu.fDamage);
		d_sfToBeAppliedValues.Add(dic["Fire Rate"], gu.fFireRate);
		d_sfToBeAppliedValues.Add(dic["Reload Speed"], gu.fReloadSpeed);
		d_sfToBeAppliedValues.Add(dic["Max Ammo"], (float) gu.iMaxAmmo);
		d_sfToBeAppliedValues.Add(dic["Idle Speed"], gu.fIdleSpeed);
		d_sfToBeAppliedValues.Add(dic["Alert Speed"], gu.fAlertSpeed);
	}

}

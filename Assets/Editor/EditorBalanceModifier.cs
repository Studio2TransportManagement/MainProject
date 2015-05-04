using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class EditorBalanceModifier : EditorWindow {

	private Vector2 vScrollPosition;

	private bool valuesFold = false;
	static private Dictionary<string, GameUnitBalance> d_units;
	static private Dictionary<string, GUIContent> d_ValuesGuiContent;
	static private List<string> l_dunitKeys; 

	static private BaseGameStructure fort;
	static private EnemySpawner enemySpawner;

	private bool gameFold = false;
	private bool showBase = false;
	private bool showSpawner = false;

	[MenuItem ("Window/Balance Prefabs")]
	static public void Init () {
		d_units = new Dictionary<string, GameUnitBalance>();
		d_ValuesGuiContent = new Dictionary<string, GUIContent>();
		l_dunitKeys = new List<string>();
		EditorBalanceModifier window =  (EditorBalanceModifier) EditorWindow.GetWindow(typeof(EditorBalanceModifier));

		d_ValuesGuiContent.Add("Health", new GUIContent("Health", "How much damage this unit can take before it will die."));
		d_ValuesGuiContent.Add("Damage", new GUIContent("Damage", "How much damage this unit will deal with each individual shot."));
		d_ValuesGuiContent.Add("Fire Rate", new GUIContent("Fire Rate", "How often this unit will fire a shot, in seconds."));
		d_ValuesGuiContent.Add("Reload Speed", new GUIContent("Reload Speed", "Time, in seconds, it takes this unit to reload and begin firing again."));
		d_ValuesGuiContent.Add("Max Ammo", new GUIContent("Max Ammo", "How many shots this unit can fire before stopping to reload."));
		d_ValuesGuiContent.Add("Idle Speed", new GUIContent("Idle Speed", "How fast this unit type will move when idling.\nEnemy Units do not Idle."));
		d_ValuesGuiContent.Add("Alert Speed", new GUIContent("Alert Speed", "How fast this unit type will move when alerted.\nEnemy Units are always alert."));

		LoadPrefabs();
		window.Show();
	}

	void OnGUI() {
		vScrollPosition = EditorGUILayout.BeginScrollView(vScrollPosition);
		GUILayout.Label("Hover for tooltips; no values of 0", EditorStyles.miniBoldLabel);
		valuesFold = EditorGUILayout.Foldout(valuesFold, "Unit Values");
		if(valuesFold) {
			EditorGUI.indentLevel++;
			l_dunitKeys = new List<string> (d_units.Keys);
			foreach(string key in l_dunitKeys) {
				d_units[key].bValuesTabOpen = EditorGUILayout.Foldout(d_units[key].bValuesTabOpen,key);
				if(d_units[key].bValuesTabOpen) {
					EditorGUILayout.BeginHorizontal();
						EditorGUILayout.BeginVertical();
							EditorGUILayout.LabelField("Values", EditorStyles.boldLabel);

							ModifyGameUnitValues(d_units[key].d_sfToBeAppliedValues);
							EditorGUILayout.BeginHorizontal();
								if(GUILayout.Button("Apply")) {
									ApplyValuesToPrefab(d_units[key]);
								}
								if(GUILayout.Button("Revert")) {
									RevertValuesToPrefab(d_units[key]);
								}
							EditorGUILayout.EndHorizontal();
						EditorGUILayout.EndVertical();
						EditorGUILayout.BeginVertical();
							EditorGUILayout.LabelField("Approx. Combat Times", EditorStyles.boldLabel);
							if(d_units[key].guGameUnit.SollyType == SOLDIER_TYPE.ENEMY_GUNNER) {
								EditorGUILayout.LabelField("vs. Base: \t\t" + d_units[key].fVsTargets[0].ToString() + "s.");
								EditorGUILayout.LabelField("vs. Gunner: \t" + d_units[key].fVsTargets[1].ToString() + "s.");
								EditorGUILayout.LabelField("vs. Heavy Gunner: \t" + d_units[key].fVsTargets[2].ToString() + "s.");
							}
							else if (d_units[key].guGameUnit.SollyType == SOLDIER_TYPE.ENEMY_TANK) {
								EditorGUILayout.LabelField("vs. Base: \t" + d_units[key].fVsTargets[0].ToString() + "s.");
							}

							else {
								EditorGUILayout.LabelField("vs. Enemy Gunner: \t" + d_units[key].fVsTargets[0].ToString() + "s.");
								EditorGUILayout.LabelField("vs. Tank: \t\t" + d_units[key].fVsTargets[1].ToString() + "s.");
								
							}

							if(GUILayout.Button("Calculate")) {
								if(d_units[key].guGameUnit.SollyType == SOLDIER_TYPE.ENEMY_GUNNER) {
									d_units[key].fVsTargets[0] = GetBattleTime(fort.fHealthMax, d_units[key].d_sfToBeAppliedValues);
									d_units[key].fVsTargets[1] = GetBattleTime(d_units["Gunner"].d_sfToBeAppliedValues[d_ValuesGuiContent["Health"]], d_units[key].d_sfToBeAppliedValues);
									d_units[key].fVsTargets[2] = GetBattleTime(d_units["Heavy Gunner"].d_sfToBeAppliedValues[d_ValuesGuiContent["Health"]], d_units[key].d_sfToBeAppliedValues);
								}
								else if (d_units[key].guGameUnit.SollyType == SOLDIER_TYPE.ENEMY_TANK) {
									d_units[key].fVsTargets[0] = GetBattleTime(fort.fHealthMax, d_units[key].d_sfToBeAppliedValues);
								}
								
								else {
									d_units[key].fVsTargets[0] = GetBattleTime(d_units["Enemy Gunner"].d_sfToBeAppliedValues[d_ValuesGuiContent["Health"]], d_units[key].d_sfToBeAppliedValues);
									d_units[key].fVsTargets[1] = GetBattleTime(d_units["Enemy Tank"].d_sfToBeAppliedValues[d_ValuesGuiContent["Health"]], d_units[key].d_sfToBeAppliedValues);	
								}
							}

						EditorGUILayout.EndVertical();
					EditorGUILayout.EndHorizontal();
				}
			}
			EditorGUI.indentLevel--;
		}

		gameFold = EditorGUILayout.Foldout(gameFold, "Spawner and Fort Values");
		if(gameFold) {
			EditorGUI.indentLevel++;
			showBase = EditorGUILayout.Foldout(showBase, "Fort");
			if(showBase) {
				EditorGUI.indentLevel++;
				GUILayout.Label("Health", EditorStyles.boldLabel);

				float tempHP = EditorGUILayout.FloatField("Starting Health: ", fort.fHealthMax);
				if(tempHP < 1) {
					tempHP = fort.fHealthMax;
				}
				else {
					fort.fHealthMax = tempHP;
				}
				int tempHUPC = EditorGUILayout.IntField("Health Upgrade Cost: $", fort.iIntegrityUpgradeCost);
				if(tempHUPC < 0) {
					tempHUPC = fort.iIntegrityUpgradeCost;
				}
				else {
					fort.iIntegrityUpgradeCost = tempHUPC;
				}
				int tempHUPA = EditorGUILayout.IntField("Health Upgrade Amount: ", fort.iIntegrityUpgradeAmount);
				if(tempHUPA < 0) {
					tempHUPA = fort.iIntegrityUpgradeAmount;
				}
				else {
					fort.iIntegrityUpgradeAmount = tempHUPA;
				}

				GUILayout.Label("Windows", EditorStyles.boldLabel);
				int tempWUPC = EditorGUILayout.IntField("Window Upgrade Cost: $", fort.iWindowUpgradeCost);
				if(tempWUPC < 0) {
					tempWUPC = fort.iWindowUpgradeCost;
				}
				else {
					fort.iWindowUpgradeCost = tempWUPC;
				}

				GUILayout.Label("Trains", EditorStyles.boldLabel);
				int tempTUPC = EditorGUILayout.IntField("Trains Upgrade Cost: $", fort.iTrainsUpgradeCost);
				if(tempTUPC < 0) {
					tempTUPC = fort.iTrainsUpgradeCost;
				}
				else {
					fort.iTrainsUpgradeCost = tempTUPC;
				}
				EditorGUI.indentLevel--;
			}

			showSpawner = EditorGUILayout.Foldout(showSpawner, "Enemy Spawner");
			if(showSpawner) {
				EditorGUI.indentLevel++;


				int tempInit = EditorGUILayout.IntField(new GUIContent ("First Wave Size: ", "How many enemies spawn with the first wave."), enemySpawner.iStartingWaveSize);
				if(tempInit < 0) {
					tempInit = enemySpawner.iStartingWaveSize;
				}
				else {
					enemySpawner.iStartingWaveSize = tempInit;
				}

				int tempInc = EditorGUILayout.IntField(new GUIContent ("Wave Increment: ", "How many units are added to the total number of enemies at the end of each wave."), enemySpawner.iWaveIncrease);
				if(tempInc < 0) {
					tempInc = enemySpawner.iWaveIncrease;
				}
				else {
					enemySpawner.iWaveIncrease = tempInc;
				}

				int tempRS = EditorGUILayout.IntField(new GUIContent ("Ratio Split Wave: ", "When the total number of units in a wave reaches this, the waves will start splitting between bases unpredictably rather than evenly"), enemySpawner.iRatioSplitWave);
				if(tempRS < 0) {
					tempRS = enemySpawner.iRatioSplitWave;
				}
				else {
					enemySpawner.iRatioSplitWave = tempRS;
				}


				float tempWR = EditorGUILayout.FloatField(new GUIContent ("Wave Rate: ", "Time, in seconds, between each wave. \nOnly starts counting once the enitre wave has spawned"), enemySpawner.fWaveRate);
				if(tempWR < 0) {
					tempWR = enemySpawner.fWaveRate;
				}
				else {
					enemySpawner.fWaveRate = tempWR;
				}

				float tempSR = EditorGUILayout.FloatField(new GUIContent ("Spawn Rate: ", "Time, in seconds, between each individual enemy unit spawn"), enemySpawner.fSpawnRate);
				if(tempSR < 0) {
					tempSR = enemySpawner.fSpawnRate;
				}
				else {
					enemySpawner.fSpawnRate = tempSR;
				}

				float tempTR = EditorGUILayout.FloatField(new GUIContent ("Tank Spawn Rate: ", "After every unit spawns, a random number from 0 to 1 is added and tracked until it reaches this value, at which point a tank spawns.\n Higher numbers mean tank spawns less often"), enemySpawner.fTankSpawnRate);
				if(tempTR < 0) {
					tempTR = enemySpawner.fTankSpawnRate;
				}
				else {
					enemySpawner.fTankSpawnRate = tempTR;
				}
				EditorGUI.indentLevel--;
			}
		}
		EditorGUILayout.EndScrollView();
	}

	static void LoadPrefabs() {

		d_units.Add("Enemy Gunner", new GameUnitBalance(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Units/EnemyGunner.prefab", (typeof(GameUnit))) as GameUnit, d_ValuesGuiContent));
		d_units.Add("Enemy Tank", new GameUnitBalance(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Units/EnemyTank.prefab", (typeof(GameUnit))) as GameUnit, d_ValuesGuiContent));
		d_units.Add("Gunner", new GameUnitBalance(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Units/Gunner.prefab", (typeof(GameUnit))) as GameUnit, d_ValuesGuiContent));
		d_units.Add("Heavy Gunner", new GameUnitBalance(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Units/Heavy Gunner.prefab", (typeof(GameUnit))) as GameUnit, d_ValuesGuiContent));
		d_units.Add("Medic", new GameUnitBalance(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Units/Medic.prefab", (typeof(GameUnit))) as GameUnit, d_ValuesGuiContent));
		d_units.Add("Mechanic", new GameUnitBalance(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Units/Mechanic.prefab", (typeof(GameUnit))) as GameUnit, d_ValuesGuiContent));

		fort = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath (AssetDatabase.FindAssets("Base", new string[] {"Assets/Prefabs"})[0]), (typeof(BaseGameStructure))) as BaseGameStructure;
		enemySpawner = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath (AssetDatabase.FindAssets("EnemySpawnController", new string[] {"Assets/Prefabs"})[0]), (typeof(EnemySpawner))) as EnemySpawner;


	}


	void ModifyGameUnitValues(Dictionary<GUIContent, float> dic) {

		List<GUIContent> keys = new List<GUIContent> (dic.Keys);

		foreach(GUIContent entry in keys) {
			float temp = EditorGUILayout.FloatField(entry, dic[entry]);
			if(temp <= 0.0f) {
				temp = dic[entry];
			}
			else {
				dic[entry] = temp;
			}
		}
	}

	void ApplyValuesToPrefab(GameUnitBalance gubal) {
		gubal.guGameUnit.fHealthMax = gubal.d_sfToBeAppliedValues[d_ValuesGuiContent["Health"]];
		gubal.guGameUnit.fDamage = gubal.d_sfToBeAppliedValues[d_ValuesGuiContent["Damage"]];
		gubal.guGameUnit.fFireRate = gubal.d_sfToBeAppliedValues[d_ValuesGuiContent["Fire Rate"]];
		gubal.guGameUnit.fReloadSpeed = gubal.d_sfToBeAppliedValues[d_ValuesGuiContent["Reload Speed"]];
		gubal.guGameUnit.iMaxAmmo = (int) gubal.d_sfToBeAppliedValues[d_ValuesGuiContent["Max Ammo"]];
		gubal.guGameUnit.fIdleSpeed = gubal.d_sfToBeAppliedValues[d_ValuesGuiContent["Idle Speed"]];
		gubal.guGameUnit.fAlertSpeed = gubal.d_sfToBeAppliedValues[d_ValuesGuiContent["Alert Speed"]];

	}

	void RevertValuesToPrefab(GameUnitBalance gubal) {
		gubal.d_sfToBeAppliedValues[d_ValuesGuiContent["Health"]] = gubal.guGameUnit.fHealthMax;
		gubal.d_sfToBeAppliedValues[d_ValuesGuiContent["Damage"]] = gubal.guGameUnit.fDamage; 
		gubal.d_sfToBeAppliedValues[d_ValuesGuiContent["Fire Rate"]] = gubal.guGameUnit.fFireRate;
		gubal.d_sfToBeAppliedValues[d_ValuesGuiContent["Reload Speed"]] = gubal.guGameUnit.fReloadSpeed;
		gubal.d_sfToBeAppliedValues[d_ValuesGuiContent["Max Ammo"]] = (float) gubal.guGameUnit.iMaxAmmo;
		gubal.d_sfToBeAppliedValues[d_ValuesGuiContent["Idle Speed"]] = gubal.guGameUnit.fIdleSpeed;
		gubal.d_sfToBeAppliedValues[d_ValuesGuiContent["Alert Speed"]] = gubal.guGameUnit.fAlertSpeed;
		
	}

	float GetBattleTime(float targetHP, Dictionary<GUIContent, float> dic){
		float battleTime = 10.0f;
		float floatCalc = 0.1f;
		int currentAmmo = (int) dic[d_ValuesGuiContent["Max Ammo"]];
		float tempFireRate = 0.0f;
		float tempReloadRate = dic[d_ValuesGuiContent["Reload Speed"]];
		bool targetDead = false;

//		if(dic[d_ValuesGuiContent["Damage"]] > 0.0f || dic[d_ValuesGuiContent["Health"]] > 0 || targetHP > 0) {
			while(!targetDead) {
				battleTime += floatCalc;
				if(targetHP >= 0) {
					if(currentAmmo > 0) {
						if(tempFireRate <= 0) {
							targetHP -= dic[d_ValuesGuiContent["Damage"]];
							currentAmmo--;
							tempFireRate = dic[d_ValuesGuiContent["Fire Rate"]];
						}
						else {
							tempFireRate -=floatCalc;
						}
					}
					else { 
						if(tempReloadRate <= 0) {
							currentAmmo = (int) dic[d_ValuesGuiContent["Max Ammo"]];
							tempReloadRate =  dic[d_ValuesGuiContent["Reload Speed"]];
						}
						else {
							tempReloadRate -=floatCalc;
						}
					}
				}
				else {
					targetDead = true;
				}
			}
//		}
		return battleTime;
	}
}

using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class EditorBalanceModifier : EditorWindow {

	private Vector2 vScrollPosition;

	private bool valuesFold = false;
	static private Dictionary<string, UnitBalanceValues> d_units;
	static private Dictionary<string, GUIContent> d_ValuesGuiContent;
	static private List<string> l_dunitKeys; 

	static private FortBalanceValues fort;
	static private SpawnerBalanceValues enemySpawner;

	private bool gameFold = false;

	[MenuItem ("Window/Balance Prefabs")]
	static public void Init () {
		d_units = new Dictionary<string, UnitBalanceValues>();
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
		l_dunitKeys = new List<string> (d_units.Keys);
		window.Show();
	}

	void OnGUI() {
		vScrollPosition = EditorGUILayout.BeginScrollView(vScrollPosition);
		GUILayout.Label("Hover for tooltips; no values of 0", EditorStyles.miniBoldLabel);
		valuesFold = EditorGUILayout.Foldout(valuesFold, "Unit Values");
		if(valuesFold) {
			EditorGUI.indentLevel++;
			foreach(string key in l_dunitKeys) {
				d_units[key].bValuesTabOpen = EditorGUILayout.Foldout(d_units[key].bValuesTabOpen,key);
				if(d_units[key].bValuesTabOpen) {
					EditorGUILayout.BeginHorizontal();
						EditorGUILayout.BeginVertical();
							EditorGUILayout.LabelField(key + "'s Values", EditorStyles.boldLabel);

							ModifyGameUnitValues(d_units[key]);
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
									d_units[key].fVsTargets[0] = Mathf.Round(GetBattleTime(fort.d_sfToBeAppliedValues["Health"], d_units[key].d_sfToBeAppliedValues, false) * 100f) / 100f;
									d_units[key].fVsTargets[1] = Mathf.Round(GetBattleTime(d_units["Gunner"].d_sfToBeAppliedValues[d_ValuesGuiContent["Health"]], d_units[key].d_sfToBeAppliedValues, false) * 100f) / 100f;
									d_units[key].fVsTargets[2] = Mathf.Round(GetBattleTime(d_units["Heavy Gunner"].d_sfToBeAppliedValues[d_ValuesGuiContent["Health"]], d_units[key].d_sfToBeAppliedValues, false) * 100f) / 100f;
								}
								else if (d_units[key].guGameUnit.SollyType == SOLDIER_TYPE.ENEMY_TANK) {
									d_units[key].fVsTargets[0] = Mathf.Round(GetBattleTime(fort.d_sfToBeAppliedValues["Health"], d_units[key].d_sfToBeAppliedValues, true) * 100f) / 100f;
								}
								
								else {
									d_units[key].fVsTargets[0] = Mathf.Round(GetBattleTime(d_units["Enemy Gunner"].d_sfToBeAppliedValues[d_ValuesGuiContent["Health"]], d_units[key].d_sfToBeAppliedValues, false)  * 100f) / 100f;
									d_units[key].fVsTargets[1] = Mathf.Round(GetBattleTime(d_units["Enemy Tank"].d_sfToBeAppliedValues[d_ValuesGuiContent["Health"]], d_units[key].d_sfToBeAppliedValues, false) * 100f) / 100f;	
								}
							}

						EditorGUILayout.EndVertical();
					EditorGUILayout.EndHorizontal();
				}
			}
			
			EditorGUILayout.BeginHorizontal();
			if(GUILayout.Button("Apply All Units")) {
				foreach(string key in l_dunitKeys) {
					ApplyValuesToPrefab(d_units[key]);
				}
			}
			if(GUILayout.Button("Revert All Units")) {
				foreach(string key in l_dunitKeys) {
					RevertValuesToPrefab(d_units[key]);
				}
			}
			EditorGUILayout.EndHorizontal();
			EditorGUI.indentLevel--;
		}

		gameFold = EditorGUILayout.Foldout(gameFold, "Spawner and Fort Values");
		if(gameFold) {
			EditorGUI.indentLevel++;
			fort.bValuesTabOpen = EditorGUILayout.Foldout(fort.bValuesTabOpen, "Fort");
			if(fort.bValuesTabOpen) {
				EditorGUILayout.LabelField("Fort's Values", EditorStyles.boldLabel);

				EditorGUI.indentLevel++;
				List<string> keys = new List<string> (fort.d_sfToBeAppliedValues.Keys);
				foreach (string key in keys) {
					float temp = EditorGUILayout.FloatField(key, fort.d_sfToBeAppliedValues[key]);
					if(temp <= 0.0f) {
						temp = fort.d_sfToBeAppliedValues[key];
					}
					else {
						fort.d_sfToBeAppliedValues[key] = temp;
					}
				}
				EditorGUILayout.BeginHorizontal();
				if(GUILayout.Button("Apply")) {
					fort.bgsBase.fHealthMax = fort.d_sfToBeAppliedValues["Health"];
					fort.bgsBase.iIntegrityUpgradeCost = (int)fort.d_sfToBeAppliedValues["Health Upg. Cost"];
					fort.bgsBase.iIntegrityUpgradeAmount = (int)fort.d_sfToBeAppliedValues["Health Upg. Amount"];
					fort.bgsBase.iWindowUpgradeCost = (int)fort.d_sfToBeAppliedValues["Window Upg. Cost"];
					fort.bgsBase.iTrainsUpgradeCost = (int)fort.d_sfToBeAppliedValues["Trains Upg. Cost"];

				}
				if(GUILayout.Button("Revert")) {
					fort.d_sfToBeAppliedValues["Health"] = fort.bgsBase.fHealthMax;
					fort.d_sfToBeAppliedValues["Health Upg. Cost"] = fort.bgsBase.iIntegrityUpgradeCost;
					fort.d_sfToBeAppliedValues["Health Upg. Amount"] = fort.bgsBase.iIntegrityUpgradeAmount;
					fort.d_sfToBeAppliedValues["Window Upg. Cost"] = fort.bgsBase.iWindowUpgradeCost;
					fort.d_sfToBeAppliedValues["Trains Upg. Cost"] = fort.bgsBase.iTrainsUpgradeCost;

				}
				EditorGUILayout.EndHorizontal();

				EditorGUI.indentLevel--;
			}

			enemySpawner.bValuesTabOpen = EditorGUILayout.Foldout(enemySpawner.bValuesTabOpen, "Enemy Spawner");
			if(enemySpawner.bValuesTabOpen) {
				EditorGUI.indentLevel++;
				EditorGUILayout.LabelField("Enemy Spawner Values", EditorStyles.boldLabel);

				List<string> keys = new List<string> (enemySpawner.d_sfToBeAppliedValues.Keys);
				foreach (string key in keys) {
					float temp = EditorGUILayout.FloatField(key, enemySpawner.d_sfToBeAppliedValues[key]);
					if(temp <= 0.0f) {
						temp = enemySpawner.d_sfToBeAppliedValues[key];
					}
					else {
						enemySpawner.d_sfToBeAppliedValues[key] = temp;
					}
				}
				EditorGUILayout.BeginHorizontal();
				if(GUILayout.Button("Apply")) {
					enemySpawner.esSpawner.iStartingWaveSize = (int)enemySpawner.d_sfToBeAppliedValues["Starting Wave Size"];
					enemySpawner.esSpawner.iWaveIncrease = (int)enemySpawner.d_sfToBeAppliedValues["Added each wave"];
					enemySpawner.esSpawner.fWaveRate = enemySpawner.d_sfToBeAppliedValues["Wave Timer"];
					enemySpawner.esSpawner.iRatioSplitWave = (int)enemySpawner.d_sfToBeAppliedValues["Ratio Split wave"];
					enemySpawner.esSpawner.fTankSpawnRate = enemySpawner.d_sfToBeAppliedValues["Tank Spawn Factor"];
					
				}
				if(GUILayout.Button("Revert")) {
					enemySpawner.d_sfToBeAppliedValues["Starting Wave Size"] = enemySpawner.esSpawner.iStartingWaveSize;
					enemySpawner.d_sfToBeAppliedValues["Added each wave"] = enemySpawner.esSpawner.iWaveIncrease;
					enemySpawner.d_sfToBeAppliedValues["Wave Timer"] = enemySpawner.esSpawner.fWaveRate;
					enemySpawner.d_sfToBeAppliedValues["Ratio Split wave"] = enemySpawner.esSpawner.iRatioSplitWave;
					enemySpawner.d_sfToBeAppliedValues["Tank Spawn Factor"] = enemySpawner.esSpawner.fTankSpawnRate;
					
				}
				EditorGUILayout.EndHorizontal();
				EditorGUI.indentLevel--;
			}
		}
		EditorGUILayout.EndScrollView();
	}

	static void LoadPrefabs() {

		d_units.Add("Enemy Gunner", new UnitBalanceValues(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Units/EnemyGunner.prefab", (typeof(GameUnit))) as GameUnit, d_ValuesGuiContent));
		d_units.Add("Enemy Tank", new UnitBalanceValues(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Units/EnemyTank.prefab", (typeof(GameUnit))) as GameUnit, d_ValuesGuiContent));
		d_units.Add("Gunner", new UnitBalanceValues(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Units/Gunner.prefab", (typeof(GameUnit))) as GameUnit, d_ValuesGuiContent));
		d_units.Add("Heavy Gunner", new UnitBalanceValues(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Units/Heavy Gunner.prefab", (typeof(GameUnit))) as GameUnit, d_ValuesGuiContent));
		d_units.Add("Medic", new UnitBalanceValues(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Units/Medic.prefab", (typeof(GameUnit))) as GameUnit, d_ValuesGuiContent));
		d_units.Add("Mechanic", new UnitBalanceValues(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Units/Mechanic.prefab", (typeof(GameUnit))) as GameUnit, d_ValuesGuiContent));

		fort = new FortBalanceValues(AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath (AssetDatabase.FindAssets("Base", new string[] {"Assets/Prefabs"})[0]), (typeof(BaseGameStructure))) as BaseGameStructure);
		enemySpawner = new SpawnerBalanceValues(AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath (AssetDatabase.FindAssets("EnemySpawnController", new string[] {"Assets/Prefabs"})[0]), (typeof(EnemySpawner))) as EnemySpawner);


	}


	void ModifyGameUnitValues(UnitBalanceValues gubal) {

		List<GUIContent> keys = new List<GUIContent> (gubal.d_sfToBeAppliedValues.Keys);

		foreach(GUIContent entry in keys) {
			float temp = EditorGUILayout.FloatField(entry, gubal.d_sfToBeAppliedValues[entry]);
			if(temp <= 0.0f) {
				temp = gubal.d_sfToBeAppliedValues[entry];
			}
			else {
				gubal.d_sfToBeAppliedValues[entry] = temp;
			}
		}

	}

	void ApplyValuesToPrefab(UnitBalanceValues gubal) {
		gubal.guGameUnit.fHealthMax = gubal.d_sfToBeAppliedValues[d_ValuesGuiContent["Health"]];
		gubal.guGameUnit.fDamage = gubal.d_sfToBeAppliedValues[d_ValuesGuiContent["Damage"]];
		gubal.guGameUnit.fFireRate = gubal.d_sfToBeAppliedValues[d_ValuesGuiContent["Fire Rate"]];
		gubal.guGameUnit.fReloadSpeed = gubal.d_sfToBeAppliedValues[d_ValuesGuiContent["Reload Speed"]];
		gubal.guGameUnit.iMaxAmmo = (int) gubal.d_sfToBeAppliedValues[d_ValuesGuiContent["Max Ammo"]];
		gubal.guGameUnit.fIdleSpeed = gubal.d_sfToBeAppliedValues[d_ValuesGuiContent["Idle Speed"]];
		gubal.guGameUnit.fAlertSpeed = gubal.d_sfToBeAppliedValues[d_ValuesGuiContent["Alert Speed"]];

	}

	void RevertValuesToPrefab(UnitBalanceValues gubal) {
		gubal.d_sfToBeAppliedValues[d_ValuesGuiContent["Health"]] = gubal.guGameUnit.fHealthMax;
		gubal.d_sfToBeAppliedValues[d_ValuesGuiContent["Damage"]] = gubal.guGameUnit.fDamage; 
		gubal.d_sfToBeAppliedValues[d_ValuesGuiContent["Fire Rate"]] = gubal.guGameUnit.fFireRate;
		gubal.d_sfToBeAppliedValues[d_ValuesGuiContent["Reload Speed"]] = gubal.guGameUnit.fReloadSpeed;
		gubal.d_sfToBeAppliedValues[d_ValuesGuiContent["Max Ammo"]] = (float) gubal.guGameUnit.iMaxAmmo;
		gubal.d_sfToBeAppliedValues[d_ValuesGuiContent["Idle Speed"]] = gubal.guGameUnit.fIdleSpeed;
		gubal.d_sfToBeAppliedValues[d_ValuesGuiContent["Alert Speed"]] = gubal.guGameUnit.fAlertSpeed;
		
	}

	float GetBattleTime(float targetHP, Dictionary<GUIContent, float> dic, bool tank){
		float battleTime = 0.0f;
		float floatCalc = 0.1f;
		int currentAmmo = (int) dic[d_ValuesGuiContent["Max Ammo"]];
		float tempFireRate = dic[d_ValuesGuiContent["Fire Rate"]];
		float tempReloadRate = dic[d_ValuesGuiContent["Reload Speed"]];
		bool targetDead = false;

		while(!targetDead) {
//			Debug.Log("Run");
			if(targetHP > 0.0f) {
				if(currentAmmo > 0) {
					if(tempFireRate <= 0.0f) {
//					Debug.Log("Damaged");
						if(tank) {
							targetHP -= dic[d_ValuesGuiContent["Damage"]] * 10.0f;
						}
						else {
							targetHP -= dic[d_ValuesGuiContent["Damage"]];
						}
						currentAmmo--;
						tempFireRate = dic[d_ValuesGuiContent["Fire Rate"]];
					}
					else {
						tempFireRate -=floatCalc;
						battleTime += floatCalc;

					}
				}
				else { 
					if(tempReloadRate <= 0.0f) {
//						Debug.Log("Reloading");
						currentAmmo = (int) dic[d_ValuesGuiContent["Max Ammo"]];
						tempReloadRate =  dic[d_ValuesGuiContent["Reload Speed"]];
					}
					else {
						tempReloadRate -=floatCalc;
						battleTime += floatCalc;

					}
				}
			}
			else {
				targetDead = true;
			}
		}
		return battleTime;
	}
}

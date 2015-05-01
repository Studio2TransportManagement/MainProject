using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class EditorBalanceModifier : EditorWindow {

	static private Vector2 vScrollPosition;

	static private EnemyUnit enemyGunner = null;
	static private EnemyUnit enemyTank = null;
	static private Gunner gunner = null;
	static private HeavyGunner heavyGunner = null;
	static private Medic medic = null;
	static private Mechanic mechanic = null;
	static private BaseGameStructure fort = null;
	static private EnemySpawner enemySpawner = null;

	//needed a bool for each different collapsable fold menu, and couldn't make them more local so and didnt realise how many I was going to make until I'd already gotten written all the way up to showing the combat times
	//Please don't kill me
	//I realise I could make this into some kinda dictionary (an array/list of straight bools would get confusing to read). 
	private bool showEG = false;
	private bool showET = false;
	private bool showG = false;
	private bool showHG = false;
	private bool showMedic = false;
	private bool showMechanic = false;

	private bool showBase = false;
	private bool showSpawner = false;

//	private bool showCEG = false;
//	private bool showCET = false;
//	private bool showCG = false;
//	private bool showCHG = false;
//	private bool showCMedic = false;
//	private bool showCMechanic = false;
	
	private bool valuesFold = false;
	private bool gameFold = false;
//	private bool combatFold = false;

	[MenuItem ("Window/Balance Window")]
	public static void Init () {
		EditorBalanceModifier window =  (EditorBalanceModifier) EditorWindow.GetWindow(typeof(EditorBalanceModifier));
		window.Show();

		LoadPrefabs();

	}

	void OnGUI() {
		vScrollPosition = EditorGUILayout.BeginScrollView(vScrollPosition);
		GUILayout.Label("(Hover for tooltips)", EditorStyles.miniBoldLabel);

		valuesFold = EditorGUILayout.Foldout(valuesFold, "Unit Values");
		if(valuesFold) {
			EditorGUI.indentLevel++;

			showEG = EditorGUILayout.Foldout(showEG, enemyGunner.name);
			if(showEG) {
				EditorGUI.indentLevel++;

				EnemyUnitValues(enemyGunner);
				EditorGUI.indentLevel--;
			}

			showET = EditorGUILayout.Foldout(showET, enemyTank.name);
			if(showET) {
				EditorGUI.indentLevel++;

				EnemyUnitValues(enemyTank);
				EditorGUI.indentLevel--;
			}

			showG = EditorGUILayout.Foldout(showG, gunner.name);
			if(showG) {
				EditorGUI.indentLevel++;

				PlayerUnitValues(gunner);
				EditorGUI.indentLevel--;
			}

			showHG = EditorGUILayout.Foldout(showHG, heavyGunner.name);
			if(showHG) {
				EditorGUI.indentLevel++;

				PlayerUnitValues(heavyGunner);
				EditorGUI.indentLevel--;
			}

			showMedic = EditorGUILayout.Foldout(showMedic, medic.name);
			if(showMedic) {
				EditorGUI.indentLevel++;

				PlayerUnitValues(medic);
				EditorGUI.indentLevel--;
			}

			showMechanic = EditorGUILayout.Foldout(showMechanic, mechanic.name);
			if(showMechanic) {
				EditorGUI.indentLevel++;

				PlayerUnitValues(mechanic);
				EditorGUI.indentLevel--;
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


				GUILayout.Label("Wave Sizes", EditorStyles.boldLabel);
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

				GUILayout.Label("Wave Rates", EditorStyles.boldLabel);

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

//		combatFold = EditorGUILayout.Foldout(combatFold, "Combat Scenarios");
//		if(combatFold) {
//			EditorGUI.indentLevel++;
//			showCEG = EditorGUILayout.Foldout(showCEG, new GUIContent (enemyGunner.name + " vs...", "Listed below is how long, in approximate seconds, it would take for this unit to defeat the opponent with full HP unhindered (no retaiation)"));
//			if(showCEG) {
//				bool calc = new bool();
//				float fFortTime;
//				EditorGUI.indentLevel++;
//				calc = GUILayout.Button("Calculate");
//				if(calc) {
//					fFortTime = GetBattleTime(fort.fHealthMax, enemyGunner);
//				}
//				GUILayout.Label("Fort", EditorStyles.miniBoldLabel);
//				GUILayout.Label(fFortTime.ToString() + "s (Approx.)");
//				EditorGUI.indentLevel--;
//			}
//			
//			showCET = EditorGUILayout.Foldout(showCET, new GUIContent (enemyTank.name + " vs...", "Listed below is how long, in seconds, it would take for this unit to defeat the opponent unhindered (no retaiation)"));
//			if(showCET) {
//				EditorGUI.indentLevel++;
//				
//				EditorGUI.indentLevel--;
//			}
//			
//			showCG = EditorGUILayout.Foldout(showCG, new GUIContent (gunner.name + " vs...", "Listed below is how long, in seconds, it would take for this unit to defeat the opponent unhindered (no retaiation)"));
//			if(showCG) {
//				EditorGUI.indentLevel++;
//				
//				EditorGUI.indentLevel--;
//			}
//			
//			showCHG = EditorGUILayout.Foldout(showCHG, new GUIContent (heavyGunner.name + " vs...", "Listed below is how long, in seconds, it would take for this unit to defeat the opponent unhindered (no retaiation)"));
//			if(showCHG) {
//				EditorGUI.indentLevel++;
//				
//				EditorGUI.indentLevel--;
//			}
//			
//			
//			EditorGUI.indentLevel--;
//		}
		EditorGUILayout.EndScrollView();
	}

	static void LoadPrefabs() {

		enemyGunner = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath (AssetDatabase.FindAssets("enemyGunner", new string[] {"Assets/Prefabs/Units"})[0]), (typeof(EnemyUnit))) as EnemyUnit;
		enemyTank = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath (AssetDatabase.FindAssets("enemyTank", new string[] {"Assets/Prefabs/Units"})[0]), (typeof(EnemyUnit))) as EnemyUnit;		
		gunner = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath (AssetDatabase.FindAssets("Gunner", new string[] {"Assets/Prefabs/Units"})[1]), (typeof(Gunner))) as Gunner;
		heavyGunner = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath (AssetDatabase.FindAssets("Heavy", new string[] {"Assets/Prefabs/Units"})[0]), (typeof(HeavyGunner))) as HeavyGunner;
		medic = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath (AssetDatabase.FindAssets("Medic", new string[] {"Assets/Prefabs/Units"})[0]), (typeof(Medic))) as Medic;
		mechanic = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath (AssetDatabase.FindAssets("Mechanic", new string[] {"Assets/Prefabs/Units"})[0]), (typeof(Mechanic))) as Mechanic;

		fort = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath (AssetDatabase.FindAssets("Base", new string[] {"Assets/Prefabs"})[0]), (typeof(BaseGameStructure))) as BaseGameStructure;
		enemySpawner = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath (AssetDatabase.FindAssets("EnemySpawnController", new string[] {"Assets/Prefabs"})[0]), (typeof(EnemySpawner))) as EnemySpawner;


	}

	void GameUnitValues(GameUnit gu) {
//		float toBeAppliedHP = gu.fHealthMax;
		GUILayout.Label(gu.name + " Health", EditorStyles.boldLabel);

		float tempHP = EditorGUILayout.FloatField("Maximum Health", gu.fHealthMax);
		if(tempHP < 1) {
			tempHP = gu.fHealthMax;
		}
		else {
			gu.fHealthMax = tempHP;
		}
		GUILayout.Label(gu.name + " Combat", EditorStyles.boldLabel);

		float tempDmg = EditorGUILayout.FloatField(new GUIContent("Damage","How much damage each shot deals."), gu.fDamage);
		if(tempDmg < 0) {
			tempDmg = gu.fDamage;
		}
		else {
			gu.fDamage = tempDmg;
		}
		float tempFR = EditorGUILayout.FloatField(new GUIContent("Fire Rate","How often a shot is fired, in seconds."), gu.fFireRate);
		if(tempFR < 0) {
			tempFR = gu.fFireRate;
		}
		else {
			gu.fFireRate = tempFR;
		}
		
		int tempA = EditorGUILayout.IntField(new GUIContent("Max Ammo","How many shots can be fired before stopping to reload."), gu.iMaxAmmo);
		if(tempA < 1) {
			tempA = gu.iMaxAmmo;
		}
		else {
			gu.iMaxAmmo = tempA;
		}
		float tempRS = EditorGUILayout.FloatField(new GUIContent("Reload Speed","Time, in seconds, it takes to reload and begin firing again"), gu.fReloadSpeed);
		if(tempRS < 1) {
			tempRS = gu.fReloadSpeed;
		}
		else {
			gu.fReloadSpeed = tempRS;
		}
		GUILayout.Label(gu.name + " Movement", EditorStyles.boldLabel);

		
	}

	void EnemyUnitValues(EnemyUnit eu) {
		GameUnitValues(eu);

		eu.navAgent.speed = EditorGUILayout.Slider(new GUIContent("Move Speed","Top Speed when moving towards base"), eu.navAgent.speed, 1.0f, 20.0f);
		EditorGUILayout.Space();
		EditorGUILayout.Space();
	}

	void PlayerUnitValues (PlayerUnit pu) {
		GameUnitValues(pu);

		pu.fIdleSpeed = EditorGUILayout.Slider(new GUIContent("Idle Speed","Top Speed when current base is standing down"), pu.fIdleSpeed, 1.0f, 20.0f);
		pu.fAlertSpeed = EditorGUILayout.Slider(new GUIContent("Alert Speed","Top Speed when current base is alerted"), pu.fAlertSpeed, 1.0f, 20.0f);
		EditorGUILayout.Space ();
		EditorGUILayout.Space();
	}

	float GetBattleTime(float targetHP, GameUnit gu){
		float battleTime = 0.0f;
		float floatCalc = 0.1f;
		int currentAmmo = gu.iMaxAmmo;
		float tempFireRate = gu.fFireRate;
		float tempReloadRate = gu.fReloadSpeed;
		bool targetDead = false;

		if(gu.fDamage > 0.0f || gu.iMaxAmmo > 0 || targetHP > 0) {
			while(!targetDead) {
				battleTime += floatCalc;
				if(targetHP > 0) {
					if(currentAmmo > 0) {
						if(tempFireRate <= 0) {
							targetHP -= gu.fDamage;
							currentAmmo--;
							tempFireRate = gu.fFireRate;
						}
						else {
							tempFireRate -=floatCalc;
						}
					}
					else { 
						if(tempReloadRate <= 0) {
							currentAmmo = gu.iMaxAmmo;
							tempReloadRate =  gu.fReloadSpeed;
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
		}
		return battleTime;
	}













}

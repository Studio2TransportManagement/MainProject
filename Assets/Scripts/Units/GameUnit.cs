﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GameUnit : MonoBehaviour, ISelectable {

	public bool bUnselectable {
		get;
		set;
	}

	public SelectionManager selectionManager {
		get;
		set;
	}

	public SOLDIER_TYPE SollyType;

    public float fHealthMax;
	public float fHealthCurrent;
	public float fRange;
	public float fFireRate;
	public float fDamage;
	
	public float fIdleSpeed;
	public float fAlertSpeed;

	public UnitAudio uaUnitAudio;

	[HideInInspector]
	public AudioSource asAudioSource;

	public GameObject goParticleOuchPrefab;
	
	public Animator aAnimator;

	public NavMeshAgent navAgent;

	[Tooltip("How many shots a unit can fire before it needs to reload")]
	public int iMaxAmmo;

	public int iCurrentAmmo{ get; set; }

	[Tooltip("The time in seconds it takes for a unit to reload (in future; a clip)")]
	public float fReloadSpeed;

	public BaseGameStructure goTargetBase { get; set; }

	public Window wMannedWindow { get; set; }

	public GameUnit guTargetUnit;
	
	public Animator goFiringEffect;

	public bool bIsFlashing = false;
	private float fTimer = 0.1f;

	public SkinnedMeshRenderer UnitsMesh;
	
	// Use this for initialization
	protected virtual void Start() {
		fHealthCurrent = fHealthMax;

		selectionManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SelectionManager>();
		UnitsMesh = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
		asAudioSource = GetComponent<AudioSource>();
		iCurrentAmmo = iMaxAmmo;
//		Debug.Log("Unit Initiated");
	}

	public bool IsSelected() {
		if (selectionManager.IsAlreadySelected(this.gameObject)) {
			return true;
		}

		return false;
	}
	
	protected virtual void Update() {

//		if (navAgent.velocity != null)
//		{
//			aAnimator.SetBool("IsWalking", true);
//		}
//		else
//		{
//			aAnimator.SetBool("IsWalking", false);
//		}

		if (bIsFlashing == true && UnitsMesh != null) {
			UnitsMesh.enabled = false;
			fTimer -= Time.deltaTime;
			
			if (fTimer <= 0) {
				UnitsMesh.enabled = true;
				bIsFlashing = false;
				fTimer = 0.1f;
			}
		}

		if (fHealthCurrent <= 0) {
			KillUnit();
		}
	}

	protected virtual void KillUnit() {
		//SOON
	}

	public void DamageUnit(float dmg, GameUnit attacker) {
		bIsFlashing = true;
		fHealthCurrent -= dmg;
		Instantiate(goParticleOuchPrefab,
		            this.transform.position + new Vector3(Random.Range(-0.5f, 0.5f),
		                                      1 + Random.Range(-0.5f, 0.5f),
		                                      Random.Range(-0.5f, 0.5f)),
		            						  Quaternion.identity
		            );

		if (this.fHealthCurrent <= 0) {
			attacker.KilledAnEnemy();
		}
	}

	public void HealUnit(float heal) {
		fHealthCurrent += heal;
		Mathf.Clamp(fHealthCurrent, 0, fHealthMax);
	}

	public void KilledAnEnemy() {
		if (this.SollyType == SOLDIER_TYPE.VILLAGER) {
			if (this.transform.localScale.x < 10) {
				this.gameObject.transform.localScale *= 1.1f;
			}
		}
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class GameUnit : MonoBehaviour, ISelectable {

	public bool bUnselectable {
		get;
		set;
	}

	public SelectionManager selectionManager {
		get;
		set;
	}

    public float fHealthMax;
	public float fHealthCurrent;
	public float fRange;
	public float fFireRate;
	public float fDamage;
	
	public Animator aAnimator;

	public NavMeshAgent navAgent;

	[Tooltip("How many shots a unit can fire before it needs to reload")]
	public int iMaxAmmo;

	public int iCurrentAmmo{ get; set; }

	[Tooltip("The time in seconds it takes for a unit to reload (in future; a clip)")]
	public int fReloadSpeed;

	public BaseGameStructure goTargetBase { get; set; }

	public Window wMannedWindow { get; set; }

	public GameUnit guTargetUnit;

	// Use this for initialization
	protected virtual void Start() {
		fHealthCurrent = fHealthMax;

		selectionManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SelectionManager>();

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

		if (fHealthCurrent > (0.25 * fHealthMax)) {
			UnitStopFlashing();
		}

		if (fHealthCurrent <= (0.25 * fHealthMax)) {
			UnitFlashing();
		}

		if (fHealthCurrent <= 0) {
			KillUnit();
		}
	}
	protected virtual void UnitStopFlashing() {
		//SOON
	}

	protected virtual void UnitFlashing() {
		//SOON
	}

	protected virtual void KillUnit() {
		//SOON
	}

	public void DamageUnit(float dmg) {
		fHealthCurrent -= dmg;
	}

	public void HealUnit(float heal) {
		fHealthCurrent += heal;
		Mathf.Clamp(fHealthCurrent, 0, fHealthMax);
	}
}

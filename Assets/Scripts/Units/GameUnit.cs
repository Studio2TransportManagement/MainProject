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
	protected float fHealthCurrent;
	public float fRange;
	public float fFireRate;
	public float fDamage;


	public NavMeshAgent navAgent;

	[Tooltip("How many shots a unit can fire before it needs to reload")]
	public int iMaxAmmo;

	public int iCurrentAmmo{ get; set; }

	[Tooltip("The time in seconds it takes for a unit to reload (in future; a clip)")]
	public int fReloadSpeed;

	public BaseStructure goTargetBase { get; set; }

	public Window wMannedWindow { get; set; }

	public GameUnit guTargetUnit { get; set; }

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
		if (fHealthCurrent <= 0) {
			KillUnit();
		}
	}
	protected virtual void KillUnit() {
		//SOON
	}

	public void DamageUnit(float dmg) {
		fHealthCurrent -=dmg;
	}
}

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


	public NavMeshAgent navAgent;
	
	public int iAmmo;
	
	public BaseStructure goTargetBase {
		get;
		set;
	}


	// Use this for initialization
	protected virtual void Start() {
		fHealthCurrent = fHealthMax;

		selectionManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SelectionManager>();

		Debug.Log("Unit Initiated");
	}

	public bool IsSelected() {
		if (selectionManager.IsAlreadySelected(this.gameObject)) {
			return true;
		}

		return false;
	}
	
	protected virtual void Update() {

		if(fHealthCurrent <= 0) {
			KillUnit();
		}

	}
	protected virtual void KillUnit() {
		//SOON
	}

	public void DamageUnit(float dmg)
	{
		fHealthCurrent -=dmg;
	}
}

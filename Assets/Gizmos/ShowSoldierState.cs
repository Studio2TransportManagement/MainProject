using UnityEngine;
using UnityEditor;
using System.Collections;

public class ShowSoldierState : MonoBehaviour {

	public Vector3 vOffset;
	public string sStateIcon = "GizmoIcon_StateSoldierIdle.png";
	public PlayerUnit puUnit;

	void OnDrawGizmos() {
		if (puUnit == null) {
			puUnit = this.GetComponent<PlayerUnit>();
		}
		else {
			Gizmos.DrawIcon(transform.position + vOffset, sStateIcon, false);

			Handles.Label(transform.position, puUnit.fHealthCurrent.ToString());
		}
	}

	void Update() {
		if (puUnit != null && puUnit.IsFSMInitialised()) {
			sStateIcon = "GizmoIcon_" + puUnit.GetStateName() + ".png";
		}
	}
}

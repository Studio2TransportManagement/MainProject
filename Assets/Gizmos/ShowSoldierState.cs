using UnityEngine;
using System.Collections;

public class ShowSoldierState : MonoBehaviour {

	private Vector3 vOffset;
	private string sStateIcon = "GizmoIcon_StateSoldierIdle.png";
	private PlayerUnit puUnit;
	private Renderer rendUnitRender;

	void OnDrawGizmos() {
		if (puUnit == null) {
			puUnit = this.GetComponent<PlayerUnit>();
			rendUnitRender = puUnit.GetComponentInChildren<Renderer>();
		}
		else {
			vOffset = new Vector3(0.0f, rendUnitRender.bounds.size.y + 2.5f, 0.0f);
			Gizmos.DrawIcon(puUnit.transform.position + vOffset, sStateIcon, false);
		}
	}

	void Update() {
		if (puUnit != null && puUnit.IsFSMInitialised()) {
			sStateIcon = "GizmoIcon_" + puUnit.GetStateName() + ".png";
		}
	}
}

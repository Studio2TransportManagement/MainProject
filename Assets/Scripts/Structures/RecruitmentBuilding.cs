using UnityEngine;
using System.Collections;

public class RecruitmentBuilding : MonoBehaviour {

	public ChildMenuController CMC;

	void OnMouseDown()
	{
		CMC.OpenCloseRecruitmentMenu();
	}
}

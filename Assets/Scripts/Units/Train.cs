using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Train : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//
	}

	// Update is called once per frame
	void Update () 
	{
		
	}

//	public void SetExpected(List<GameUnit> lexp) {
//		l_guExpected = lexp;
//	}
//
//	public void AddExpected (GameUnit unit) {
//		l_guExpected.Add(unit);
//	}
//
//	public void ClearExpected() {
//		l_guExpected.Clear();
//	}
//
//	public void SetPassengers(List<GameUnit> lpass) {
//		l_guPassengers = lpass;
//		foreach (GameUnit gunit in l_guPassengers) {
//			gunit.gameObject.SetActive(false);
//		}
//	}
//
//	public void DumpPassengers() {
//		//Drop the people back into the world
//		foreach (GameUnit gunit in l_guPassengers) {
//			gunit.GetComponent<SlideToLocation>().vTarget = this.transform.position;
//			gunit.transform.position = this.transform.position;
//			gunit.gameObject.SetActive(true);
//		}
//	}
//
//	public void ClearPassengers() {
//		l_guPassengers.Clear();
//	}
}

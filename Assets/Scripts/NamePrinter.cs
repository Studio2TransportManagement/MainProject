using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NamePrinter : MonoBehaviour {

	public Text[] l_TFallenSoldierNames;
	public NameSaver MWNameSaver;

	void Start ()
	{
		MWNameSaver = GameObject.FindGameObjectWithTag("NameSaver").GetComponent<NameSaver>();
	}

	void Update () 
	{
		l_TFallenSoldierNames[0].text = "Saul Alexander";
		l_TFallenSoldierNames[1].text = "Lachlan Brown";
		l_TFallenSoldierNames[2].text = "Kouta Cles";
		l_TFallenSoldierNames[3].text = "Duncan Corrigan";
		l_TFallenSoldierNames[4].text = "David Hanna";
		l_TFallenSoldierNames[5].text = "Scott Neskovski";

		for(int i = 6; i < MWNameSaver.l_sDeadUnitNames.Count; i++)
		{
			l_TFallenSoldierNames[i].text = MWNameSaver.l_sDeadUnitNames[i];
		}

//		if (Input.GetKeyDown(KeyCode.P))
//		{
//			for(int i = 6; i < l_TFallenSoldierNames.Length; i++)
//			{
//				l_TFallenSoldierNames[i].text = "Placeholder Name";
//			}
//		}
	}
}

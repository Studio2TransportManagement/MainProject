using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NamePrinter : MonoBehaviour {

	public Text[] l_TFallenSoldierNames;
	public NameSaver MWNameSaver;

	void Start ()
	{
//		MWNameSaver = GameObject.FindGameObjectWithTag("NameSaver").GetComponent<NameSaver>();
	}

	void Update () 
	{
//		for(int i = 0; i < MWNameSaver.l_sDeadUnitNames.Count; i++)
//		{
//			l_TFallenSoldierNames[i].text = MWNameSaver.l_sDeadUnitNames[i];
//		}

		if (Input.GetKeyDown(KeyCode.P))
		{
			for(int i = 0; i < l_TFallenSoldierNames.Length; i++)
			{
				l_TFallenSoldierNames[i].text = "Placeholder Name";
			}
		}
	}
}

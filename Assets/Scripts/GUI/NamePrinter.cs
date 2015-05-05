using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NamePrinter : MonoBehaviour {

	public GameObject goMemorialWall;
	private bool bisStartofScene = true;
	private float fRiseTimer = 3.0f;
	public Text[] l_TFallenSoldierNames;
	public NameSaver MWNameSaver;

	void Start ()
	{
		l_TFallenSoldierNames[0].text = "Saul Alexander";
		l_TFallenSoldierNames[1].text = "Lachlan Brown";
		l_TFallenSoldierNames[2].text = "Kouta Cles";
		l_TFallenSoldierNames[3].text = "Duncan Corrigan";
		l_TFallenSoldierNames[4].text = "David Hanna";
		l_TFallenSoldierNames[5].text = "Scott Neskovski";
//		MWNameSaver = GameObject.FindGameObjectWithTag("NameSaver").GetComponent<NameSaver>();
	}

	void Update () 
	{
		if(bisStartofScene)
		{
			fRiseTimer -= Time.deltaTime;
			
			if(fRiseTimer <= 0f)
			{
				MoveMemorialWall();
				bisStartofScene = false;
			}
		}
//		for(int i = 6; (i-6) < MWNameSaver.l_sDeadUnitNames.Count; i++)
//		{
//			l_TFallenSoldierNames[i].text = MWNameSaver.l_sDeadUnitNames[i-6];
//		}

//		if (Input.GetKeyDown(KeyCode.P))
//		{
//			for(int i = 6; i < l_TFallenSoldierNames.Length; i++)
//			{
//				l_TFallenSoldierNames[i].text = "Placeholder Name";
//			}
//		}
	}

	void MoveMemorialWall ()
	{
		LeanTween.move (goMemorialWall, new Vector2 (Screen.width/2f, (Screen.height/2f) - 40), 3f).setEase (LeanTweenType.easeInQuad);
	}
}

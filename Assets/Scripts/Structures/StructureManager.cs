using UnityEngine;
using System.Collections;

public class StructureManager : MonoBehaviour {

	public static GameObject[] bases;

	baseBuilding[] baseBuildings;

//	public GameObject[] Bases
//	{
//		get
//		{
//			return bases;
//		}
//	}
//
	public float fBaseStartingHealth;


	void Awake () {
		baseBuildings = FindObjectsOfType(typeof(baseBuilding)) as baseBuilding[];

		bases = new GameObject[baseBuildings.Length];

		for(int i = 0; i < baseBuildings.Length; i++)
		{
			bases[i] = baseBuildings[i].gameObject;
		}
	}

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class baseBuilding : MonoBehaviour {

	private float health;

	public float Health 
	{
		get
		{
			return health;
		}

		set
		{
			health = value;
		}
	}

	void Awake () {

	}
	// Use this for initialization
	void Start () {
	
	}

}

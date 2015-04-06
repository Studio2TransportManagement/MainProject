using UnityEngine;
using System.Collections.Generic;

public class NameSaver : MonoBehaviour {
	
	public static NameSaver Instance;
	
	public List<string> l_guDeadUnitNames;

	void Start() 
	{
		l_guDeadUnitNames = new List<string> ();
	}

	void Update ()
	{

	}
	
	void Awake ()
	{
		if(Instance)
		{
			DestroyImmediate(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}
	}
}

using UnityEngine;
using System.Collections;

public class baseStructure : GameStructure {

	void Start () 
	{
		staticStructures.bases.Add(this);
	}

}

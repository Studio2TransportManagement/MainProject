using UnityEngine;
using System.Collections;

public class CapacityCounter : MonoBehaviour {

	public RectTransform canvasRectT;
	public RectTransform TrainsRectT;
	public Transform tObjectToFollow;

	// Use this for initialization
	void Start () {
	
	}

	void Update () 
	{
		Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, tObjectToFollow.position + new Vector3 (0f, 0f, 0f));
		
		TrainsRectT.anchoredPosition = screenPoint - canvasRectT.sizeDelta / 2f;
	}
}

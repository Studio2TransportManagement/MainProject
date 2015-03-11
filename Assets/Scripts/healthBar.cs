using UnityEngine;
using System.Collections;

public class healthBar : MonoBehaviour {
	
	public RectTransform canvasRectT;
	public RectTransform healthBarRectT;
	public Transform objectToFollow;
	
	void Update()
	{
		Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, objectToFollow.position + new Vector3 (0f, 0.25f, 0f));
		
		healthBarRectT.anchoredPosition = screenPoint - canvasRectT.sizeDelta / 2f;
	}
}

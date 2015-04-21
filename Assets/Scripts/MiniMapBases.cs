using UnityEngine;
using System.Collections;

public class MiniMapBases : MonoBehaviour {

	public Camera MiniMapCamera;
	public RectTransform canvasRectT;
	public RectTransform BasesRectT;
	public Transform tObjectToFollow;
	
	void Update()
	{
		Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(MiniMapCamera, tObjectToFollow.position + new Vector3 (0f, 0f, 0f));
		
		BasesRectT.anchoredPosition = screenPoint - canvasRectT.sizeDelta / 2f;
	}
}

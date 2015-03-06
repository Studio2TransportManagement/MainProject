using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class dragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public static GameObject itemBeingDragged;
	Vector3 startPosition;
	Vector3 startSize;
	Vector3 dragSize;

	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData eventData)
	{
		startSize.x = 0.8109928f;
		startSize.y = 0.7791893f;
		startSize.z = 0.9934663f;
		dragSize.x = 0.2585708f;
		dragSize.y = 0.2484309f;
		dragSize.z = 0.3167494f;
		itemBeingDragged = gameObject;
		startPosition = transform.position;
		itemBeingDragged.transform.localScale = dragSize;
	}
	#endregion


	#region IDragHandler implementation
	public void OnDrag (PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}
	#endregion

	#region IEndDragHandler implementation
	public void OnEndDrag (PointerEventData eventData)
	{
		itemBeingDragged.transform.localScale = startSize;
		transform.position = startPosition;
		itemBeingDragged = null;
	}
	#endregion

}

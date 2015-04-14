using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class dragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public UIAudioManager audioManager;
	public static GameObject itemBeingDragged;
	Vector3 startPosition;
	Vector3 startSize;
	Vector3 dragSize;

	void Start ()
	{
		audioManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIAudioManager>();
	}

	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData eventData)
	{
		startSize.x = 0.6618425f;
		startSize.y = 0.6358879f;
		startSize.z = 0.8107571f;
		dragSize.x = 0.2585708f;
		dragSize.y = 0.2484309f;
		dragSize.z = 0.3167494f;
		itemBeingDragged = gameObject;
		startPosition = transform.position;
		itemBeingDragged.transform.localScale = dragSize;
		AudioSource.PlayClipAtPoint (audioManager.acGrabRecruit, Camera.main.transform.position);
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

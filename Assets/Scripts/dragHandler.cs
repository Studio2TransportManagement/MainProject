using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class dragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public UIAudioManager audioManager;
	public static GameObject goItemBeingDragged;
	public GameObject goRecruitmentInfoPanel;
	Vector3 vStartPosition;
	Vector3 vStartSize;
	Vector3 vDragSize;

	void Start ()
	{
		audioManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIAudioManager>();
	}

	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData eventData)
	{
		vStartSize.x = 0.6618425f;
		vStartSize.y = 0.6358879f;
		vStartSize.z = 0.8107571f;
		vDragSize.x = 0.2585708f;
		vDragSize.y = 0.2484309f;
		vDragSize.z = 0.3167494f;
		goItemBeingDragged = gameObject;
		vStartPosition = transform.position;
		goItemBeingDragged.transform.localScale = vDragSize;
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
		goItemBeingDragged.transform.localScale = vStartSize;
		transform.position = vStartPosition;
		goItemBeingDragged = null;
		if(goRecruitmentInfoPanel.activeInHierarchy)
		{
			goRecruitmentInfoPanel.SetActive (false);
		}
	}
	#endregion

}

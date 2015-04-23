using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MiniMapBases : MonoBehaviour {

	public Camera MiniMapCamera;
	public RectTransform canvasRectT;
	public RectTransform BasesRectT;
	public Transform tObjectToFollow;
	public BaseGameStructure ThisBase;
	public Image ICurrentBase;
	public Sprite sBlue;
	public Sprite sOrange;
	public Sprite sRed;
	public Sprite sBlinkingRed;
	
	void Update()
	{
		Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(MiniMapCamera, tObjectToFollow.position + new Vector3 (0f, 0f, 0f));
		
		BasesRectT.anchoredPosition = screenPoint - canvasRectT.sizeDelta / 2f;

		if(ThisBase.fHealthCurrent <= ThisBase.fHealthMax &&
		   ThisBase.fHealthCurrent > (3f/4f) * ThisBase.fHealthMax)
		{
			ICurrentBase.overrideSprite = sBlue;
		}

		if(ThisBase.fHealthCurrent <= (3f/4f) * ThisBase.fHealthMax &&
		   ThisBase.fHealthCurrent > (1f/2f) * ThisBase.fHealthMax)
		{
			ICurrentBase.overrideSprite = sOrange;
		}

		if(ThisBase.fHealthCurrent <= (1f/2f) * ThisBase.fHealthMax &&
		   ThisBase.fHealthCurrent > (1f/4f) * ThisBase.fHealthMax)
		{
			ICurrentBase.overrideSprite = sRed;
		}

		if(ThisBase.fHealthCurrent <= (1f/4f) * ThisBase.fHealthMax)
		{
			ICurrentBase.overrideSprite = sBlinkingRed;
		}
	}
}

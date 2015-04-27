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
	public Sprite sBlinkingRed0;
	public Sprite sBlinkingRed1;
	public Sprite sBlinkingRed2;
	public Sprite sBlinkingRed3;
	public Sprite sBlinkingRed4;
	public Sprite sBlinkingRed5;
	public Sprite sBlinkingRed6;
	public float fTimer = 0.875f;
	
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
			ICurrentBase.overrideSprite = sBlinkingRed0;
			fTimer -= Time.deltaTime;

			if(fTimer <= 0.75f)
			{
				ICurrentBase.overrideSprite = sBlinkingRed1;
			}
			if(fTimer <= 0.625f)
			{
				ICurrentBase.overrideSprite = sBlinkingRed2;
			}
			if(fTimer <= 0.5f)
			{
				ICurrentBase.overrideSprite = sBlinkingRed3;
			}
			if(fTimer <= 0.375f)
			{
				ICurrentBase.overrideSprite = sBlinkingRed4;
			}
			if(fTimer <= 0.250f)
			{
				ICurrentBase.overrideSprite = sBlinkingRed5;
			}
			if(fTimer <= 0.125f)
			{
				ICurrentBase.overrideSprite = sBlinkingRed6;
			}
			if(fTimer <= 0f)
			{
				ICurrentBase.overrideSprite = sBlinkingRed0;
				fTimer = 0.875f;
			}
		}
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DragTutorial : MonoBehaviour {

	public Image IThisImage;
	public Sprite[] DragSprites; 
	private float fTimer = 3.68f;

	void Update ()
	{
		IThisImage.overrideSprite = DragSprites[0];
		fTimer -= Time.deltaTime;
		
		if(fTimer <= 3.52f)
		{
			IThisImage.overrideSprite = DragSprites[1];
		}
		if(fTimer <= 3.36f)
		{
			IThisImage.overrideSprite = DragSprites[2];
		}
		if(fTimer <= 3.2f)
		{
			IThisImage.overrideSprite = DragSprites[3];
		}
		if(fTimer <= 3.04f)
		{
			IThisImage.overrideSprite = DragSprites[4];
		}
		if(fTimer <= 2.88f)
		{
			IThisImage.overrideSprite = DragSprites[5];
		}
		if(fTimer <= 2.72f)
		{
			IThisImage.overrideSprite = DragSprites[6];
		}
		if(fTimer <= 2.56f)
		{
			IThisImage.overrideSprite = DragSprites[7];
		}
		if(fTimer <= 2.4f)
		{
			IThisImage.overrideSprite = DragSprites[8];
		}
		if(fTimer <= 2.24f)
		{
			IThisImage.overrideSprite = DragSprites[9];
		}
		if(fTimer <= 2.08f)
		{
			IThisImage.overrideSprite = DragSprites[10];
		}
		if(fTimer <= 1.92f)
		{
			IThisImage.overrideSprite = DragSprites[11];
		}
		if(fTimer <= 1.76f)
		{
			IThisImage.overrideSprite = DragSprites[12];
		}
		if(fTimer <= 1.6f)
		{
			IThisImage.overrideSprite = DragSprites[13];
		}
		if(fTimer <= 1.44f)
		{
			IThisImage.overrideSprite = DragSprites[14];
		}
		if(fTimer <= 1.28f)
		{
			IThisImage.overrideSprite = DragSprites[15];
		}
		if(fTimer <= 1.12f)
		{
			IThisImage.overrideSprite = DragSprites[16];
		}
		if(fTimer <= 0.96f)
		{
			IThisImage.overrideSprite = DragSprites[17];
		}
		if(fTimer <= 0.8f)
		{
			IThisImage.overrideSprite = DragSprites[18];
		}
		if(fTimer <= 0.64f)
		{
			IThisImage.overrideSprite = DragSprites[19];
		}
		if(fTimer <= 0.48f)
		{
			IThisImage.overrideSprite = DragSprites[20];
		}
		if(fTimer <= 0.32f)
		{
			IThisImage.overrideSprite = DragSprites[21];
		}
		if(fTimer <= 0.16f)
		{
			IThisImage.overrideSprite = DragSprites[22];
		}
		if(fTimer <= 0f)
		{
			IThisImage.overrideSprite = DragSprites[23];
			fTimer = 3.68f;
		}
	}
}

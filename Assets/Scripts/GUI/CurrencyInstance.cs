using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CurrencyInstance : MonoBehaviour {

	public Text tThisText;

	void Start ()
	{
		tThisText = gameObject.GetComponent<Text> ();
		LeanTween.move (gameObject, gameObject.transform.position + new Vector3(0f, 20f, 0f), 2f).setEase (LeanTweenType.easeInQuad);
	}

	void Update () 
	{
		Color32 fadeColor = tThisText.color;
		fadeColor.a -= 2;
		tThisText.color = fadeColor;
		
		if(tThisText.color.a <= 0)
		{
			Destroy (gameObject);
		}
	}
}

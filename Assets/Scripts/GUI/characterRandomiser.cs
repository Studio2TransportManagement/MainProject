using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using UnityEngine.UI;

public class characterRandomiser : MonoBehaviour {

	private PlayerResources pPlayerResources;
	public Sprite[] characterModels;
	public string[] characterFirstNames;
	public string[] characterLastNames;
	public Image IRecruit;
	public Image IDraggableRecruit;
	public Text TCharacterName;
	public GameObject goInstructions;
	public TextAsset firstNames;
	public TextAsset lastNames;

	void Awake () {
		pPlayerResources = FindObjectOfType<PlayerResources>();
	}

	public void Start ()
	{
		try 
		{
			characterFirstNames = firstNames.text.Split('\n');
			characterLastNames = lastNames.text.Split('\n');
		}
		catch(IOException e)
		{
			Debug.Log("<color=red>Read error! </color> " + e.Message);
		}

		int modelNumber = Random.Range (0, characterModels.Length);
		
		IRecruit.overrideSprite = characterModels [modelNumber];
		IDraggableRecruit.overrideSprite = characterModels [modelNumber];
		
		int nameNumber = Random.Range (0, characterFirstNames.Length);
		
		TCharacterName.text = "" + characterFirstNames [nameNumber];
		
		nameNumber = Random.Range (0, characterLastNames.Length);
		
		TCharacterName.text += " " + characterLastNames [nameNumber];
	}

	public void Update ()
	{
		if(pPlayerResources.GetRecruits() == 0)
		{
			IRecruit.transform.gameObject.SetActive (false);
			IDraggableRecruit.transform.gameObject.SetActive (false);
			TCharacterName.transform.gameObject.SetActive (false);
			goInstructions.SetActive (false);
		}

		if(pPlayerResources.GetRecruits() > 0)
		{
			IRecruit.transform.gameObject.SetActive (true);
			IDraggableRecruit.transform.gameObject.SetActive (true);
			TCharacterName.transform.gameObject.SetActive (true);
			goInstructions.SetActive (true);
		}
	}

	public void RandomiseAvatar ()
	{
		int modelNumber = Random.Range (0, characterModels.Length);

		IRecruit.overrideSprite = characterModels [modelNumber];
		IDraggableRecruit.overrideSprite = characterModels [modelNumber];

		int nameNumber = Random.Range (0, characterFirstNames.Length);
	   
		TCharacterName.text = "" + characterFirstNames [nameNumber];

		nameNumber = Random.Range (0, characterLastNames.Length);

		TCharacterName.text += " " + characterLastNames [nameNumber];
	}
}

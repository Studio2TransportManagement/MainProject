using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using UnityEngine.UI;

public class characterRandomiser : MonoBehaviour {
	
	public Sprite[] characterModels;
	public string[] characterFirstNames;
	public string[] characterLastNames;
	public GameObject Recruit;
	public GameObject DraggableRecruit;
	public GameObject characterName;

	public void Start ()
	{
		try 
		{
			characterFirstNames = File.ReadAllLines("Assets/Scripts/SoldierFirstNames.txt");
			characterLastNames = File.ReadAllLines("Assets/Scripts/SoldierLastNames.txt");
		}
		catch(IOException e)
		{
			Debug.Log("<color=red>Read error! </color> " + e.Message);
		}

		int modelNumber = Random.Range (0, characterModels.Length);
		
		Recruit.GetComponent<Image> ().overrideSprite = characterModels [modelNumber];
		DraggableRecruit.GetComponent<Image> ().overrideSprite = characterModels [modelNumber];
		
		int nameNumber = Random.Range (0, characterFirstNames.Length);
		
		characterName.GetComponent<Text> ().text = "" + characterFirstNames [nameNumber];
		
		nameNumber = Random.Range (0, characterLastNames.Length);
		
		characterName.GetComponent<Text> ().text += " " + characterLastNames [nameNumber];
	}

	public void RandomiseAvatar ()
	{
		int modelNumber = Random.Range (0, characterModels.Length);

		Recruit.GetComponent<Image> ().overrideSprite = characterModels [modelNumber];
		DraggableRecruit.GetComponent<Image> ().overrideSprite = characterModels [modelNumber];

		int nameNumber = Random.Range (0, characterFirstNames.Length);
	   
		characterName.GetComponent<Text> ().text = "" + characterFirstNames [nameNumber];

		nameNumber = Random.Range (0, characterLastNames.Length);

		characterName.GetComponent<Text> ().text += " " + characterLastNames [nameNumber];
	}
}

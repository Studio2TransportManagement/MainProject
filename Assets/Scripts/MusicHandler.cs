using UnityEngine;
using System.Collections;

public class MusicHandler : MonoBehaviour {

	public AudioSource asBackgroundMusic;
	public AudioSource asDemotivationalPhrases;
	public float fDimTimerInMinutes = 1;
	
	void Start ()
	{
		fDimTimerInMinutes = fDimTimerInMinutes * 60f;
	}
	
	void Update ()
	{
		asBackgroundMusic.volume -= Time.deltaTime / fDimTimerInMinutes;
		asDemotivationalPhrases.volume += Time.deltaTime / fDimTimerInMinutes;
	}
}

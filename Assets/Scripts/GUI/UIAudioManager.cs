using UnityEngine;
using System.Collections;

public class UIAudioManager : MonoBehaviour {

	public AudioClip acOpenUpgrade;
	public AudioClip acCloseUpgrade;
	public AudioClip acChangingBase;
	public AudioClip acSwitchingMenus;
	public AudioClip acOpenRecruitment;
	public AudioClip acCloseRecruitment;
	public AudioClip acUpgradeButton;
	public AudioClip acGrabRecruit;
	public AudioClip acStartTraining;
	public AudioClip acEndTraining;
	public AudioClip acEnterTower;
	public AudioClip acLeaveTower;
	public AudioClip acSelectUnit;
	public AudioClip acDeselectUnit;
	public AudioClip acThomasButton;

	public void UpgradeButton ()
	{
		AudioSource.PlayClipAtPoint( acUpgradeButton, Camera.main.transform.position);
	}

	public void ThomasButton ()
	{
		AudioSource.PlayClipAtPoint( acThomasButton, Camera.main.transform.position);
	}

}

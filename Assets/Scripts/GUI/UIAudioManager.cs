using UnityEngine;
using System.Collections;

public class UIAudioManager : MonoBehaviour {

	public AudioClip ACOpenUpgrade;
	public AudioClip ACCloseUpgrade;
	public AudioClip ACChangingBase;
	public AudioClip ACSwitchingMenus;
	public AudioClip ACOpenRecruitment;
	public AudioClip ACCloseRecruitment;
	public AudioClip ACUpgradeButton;
	public AudioClip ACGrabRecruit;
	public AudioClip ACStartTraining;
	public AudioClip ACEndTraining;
	public AudioClip ACEnterTower;
	public AudioClip ACLeaveTower;
	public AudioClip ACSelectUnit;
	public AudioClip ACDeselectUnit;

	public void UpgradeButton ()
	{
		AudioSource.PlayClipAtPoint( ACUpgradeButton, Camera.main.transform.position);
	}
}

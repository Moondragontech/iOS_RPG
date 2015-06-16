using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hud : MonoBehaviour 
{

	public Text labelLevel;
	public Text labelGold;
	public Slider SliderXP;

	void Update()
	{
		labelLevel.text = "LEVEL : " + PlayerData.GetInstance().mPartyLevel;
		labelGold.text = "GOLD : " + PlayerData.GetInstance().mPartyGold;

		float xp = (float)PlayerData.GetInstance().mPartyExperience;
		SliderXP.value = (xp/100.0f);
	}

	public void OnSave()
	{
		PlayerData.GetInstance().Save();
	}

	public void OnLoad()
	{
		PlayerData.GetInstance().Load();
	}
}

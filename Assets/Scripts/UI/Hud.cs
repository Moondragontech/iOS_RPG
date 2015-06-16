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
		labelLevel.text = "LEVEL : " + PlayerData.GetInstance().mPlayer1Level;
		labelGold.text = "GOLD : " + PlayerData.GetInstance().mPartyGold;

		float xp = (float)PlayerData.GetInstance().mPlayer1CurrentExperience;
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

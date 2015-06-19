using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
	
	public Text labelLevel;
	public Text labelGold;
	public Slider SliderXP;
	
	void Update ()
	{
		labelLevel.text = "LEVEL : " + PlayerData.GetInstance ().partyLevel;
		labelGold.text = "GOLD : " + PlayerData.GetInstance ().partyGold;
		
		float xp = (float)PlayerData.GetInstance ().partyExperience;
		SliderXP.value = (xp / 100.0f);
	}
	
	public void OnSave ()
	{
		PlayerData.GetInstance ().Save ();
	}
	
	public void OnLoad ()
	{
		PlayerData.GetInstance ().Load ();
	}
}


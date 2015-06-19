using UnityEngine;
using System.Collections;

public class dataAccess : MonoBehaviour
{
	
	public static dataAccess info;	//self reference to singleton	
	private PlayerData mPlayerData;	//reference to the player data
	
	//init before start
	void Awake ()
	{
		//singleton
		if (info == null) {
			info = this;
		}
	}
	
	// Use this for initialization
	void Start ()
	{
		//referencing player data
		mPlayerData = PlayerData.GetInstance ();
	}
	
	/// <summary>
	/// Updates the gold.
	/// </summary>
	/// <param name="Amount">Amount.</param>
	public void updateGold (int Amount) // this is int to allow gold deduction
	{
		if (mPlayerData != null) {
			mPlayerData.partyGold += (uint)Amount;
		}
	}
	
	/// <summary>
	/// Updates the xp.
	/// </summary>
	/// <param name="Amount">Amount.</param>
	public void updateXp (uint Amount)
	{
		if (mPlayerData != null) {
			mPlayerData.partyExperience += Amount;
		}
		
	}
	
	/// <summary>
	/// Updates the player level
	/// </summary>
	/// <param name="Amount">Amount.</param>
	public void updateLevel (uint Amount)
	{
		if (mPlayerData != null) {
			mPlayerData.partyLevel += Amount;
		}
	}
}

using UnityEngine;
using System.Collections;

public class dataAccess : MonoBehaviour
{

	public static dataAccess info;	//self reference to singleton
	
	public int baseXPGranted = 20;	//base xp grated to the winner
	
	private PlayerData mPlayerData;
	
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
	public void updateGold (int Amount)
	{
		if (mPlayerData != null) {
			mPlayerData.mPartyGold = (uint)Mathf.Clamp (((int)mPlayerData.mPartyGold + Amount), 0, 10000);	//->later 10000 should be replaced by a max variable
			Debug.Log ("gold: " + (int)mPlayerData.mPartyGold);
		}
	}
	
	/// <summary>
	/// Updates the xp.
	/// </summary>
	/// <param name="Amount">Amount.</param>
	public void updateXp (uint Amount)
	{
		if (mPlayerData != null) {
			mPlayerData.mPartyExperience += (uint)Amount;
		}
	}
	
	/// <summary>
	/// Increases the exp on enemy kill.
	/// </summary>
	/// <param name="killedCharacterLevel">Killed character level.</param>
	public void increaseExpOnEnemyKill (int killedCharacterLevel)
	{
		if (mPlayerData != null) {
			mPlayerData.mPartyExperience += (uint)(killedCharacterLevel * baseXPGranted);	//with respect to enemy killed
		}
		
	}
	
	/// <summary>
	/// Updates the player level
	/// </summary>
	/// <param name="Amount">Amount.</param>
	public void updateLevel (uint Amount)
	{
		if (mPlayerData != null) {
			mPlayerData.mPartyLevel += (uint)Amount;
		}
	}
}

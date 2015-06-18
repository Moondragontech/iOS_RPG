using UnityEngine;
using System.Collections;

public class Enemy : Character
{
	[Tooltip("base gold given to player on death")]
	public int
		baseGoldDrop = 50;	//base gold given to player on death
	/// <summary>
	/// when this character is activated. 
	/// </summary>
	public override void onActivated (GameObject currentTarget, bool bActive)
	{
		base.onActivated (currentTarget, bActive);
	
		battleInfo.info.enemyFlag.GetComponent<ActiveCharacterInfo> ().setActivateArrow (this.transform);
		
		if (currentTarget != null)	//if only valid
			battleInfo.info.playerFlag.GetComponent<ActiveCharacterInfo> ().setActivateArrow (currentTarget.transform);
	}
	
	/// <summary>
	/// overriding on death. 
	/// </summary>
	public override void onDeath ()
	{
		base.onDeath ();
		
		//giving exp to player when enemy dies
		dataAccess.info.increaseExpOnEnemyKill (characterLevel);
		
		//giving gold when enemies die
		dataAccess.info.updateGold (characterLevel * baseGoldDrop);
		
		//destroying the gameobject
		Destroy (this.gameObject);
	}
}

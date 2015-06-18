using UnityEngine;
using System.Collections;

public class FPlayer : Character
{
	[Tooltip("base gold decreases upon death")]
	public int
		baseGoldPanaltyOnDeath = 30;		//base gold decreases upon death
	
	/// <summary>
	/// when this character is activated. 
	/// </summary>
	public override void onActivated (GameObject currentTarget, bool bActive)
	{
		base.onActivated (currentTarget, bActive);
		
		battleInfo.info.playerFlag.GetComponent<ActiveCharacterInfo> ().setActivateArrow (this.transform);
		
		if (currentTarget != null)		//only if valid
			battleInfo.info.enemyFlag.GetComponent<ActiveCharacterInfo> ().setActivateArrow (currentTarget.transform);
	}
	
	/// <summary>
	/// overriding on death. 
	/// </summary>
	public override void onDeath ()
	{
		base.onDeath ();
		
		//giving gold when enemies die
		dataAccess.info.updateGold (-characterLevel * baseGoldPanaltyOnDeath);
		
		//destroying the gameobject
		Destroy (this.gameObject);
	}
}

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
		
		battleController.info.playerFlag.GetComponent<ActiveCharacterInfo> ().setActivateArrow (this.transform);
		
		if (currentTarget != null)		//only if valid
			battleController.info.enemyFlag.GetComponent<ActiveCharacterInfo> ().setActivateArrow (currentTarget.transform);
	}
	
	/// <summary>
	/// overriding on death. 
	/// </summary>
	public override void onDeath ()
	{
		base.onDeath ();
		
		//decrease gold when ally dies
		battleController.info.decreaseGoldOnPlayerPartyDeath (characterLevel);
		
		//destroying the gameobject
		Destroy (this.gameObject);
	}
}

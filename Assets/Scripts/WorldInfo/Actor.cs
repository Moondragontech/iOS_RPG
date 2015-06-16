using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour
{
	// basic attributes---------
	public float health = 100;			//current health of the player
	public float maxHealth = 100;		//max health a character can have
	//-------
	
	//events-----------
	public event EventNotification Death;	//event to notify on death


	//basic methods-----------
	
	/// <summary>
	/// Take damage, calling this method will deal damage to this actor/character
	/// </summary>
	/// <param name="inDamage">In damage.</param>
	/// <param name="damageDealer">Damage dealer.</param>
	/// <param name="Instigator">Instigator (damage dealer class, only if a character).</param>
	virtual public void takeDamage (float inDamage, GameObject damageDealer, Actor instigator = null)
	{
		//basic implementation
		//further implementation in the subclass may have armor and damage reduction features
		health = Mathf.Clamp (health - inDamage, 0, maxHealth);	//clamping the resulting value to go beyond 0
		
		//if health is 0, calling the death
		if (health <= 0) {
			onDeath ();
		}
	}
	
	/// <summary>
	/// Do damage to the target. simplified use
	/// </summary>
	/// <param name="damage">Damage.</param>
	/// <param name="target">Damaged actor.</param>
	public void doDamage (float damage, GameObject target)
	{
		if (target.GetComponent<Actor> () == null)	//early exit if the actor component is not found (safety)
			return;
			
		target.GetComponent<Actor> ().takeDamage (damage, target, this);
	}
	
	
	/// <summary>
	/// on death. It can be overriden on the subclass for specific actions
	/// </summary>
	virtual public void onDeath ()
	{
		//logging out
		//Debug.Log ("Actor died: " + this.gameObject.ToString ());
		
		//calling the event
		eventDeath ();
	}
	
	
	///events------------------
	/// <summary>
	/// call event on Death
	/// </summary>
	protected internal void eventDeath ()
	{
		//sends a message to the GameInfo/battleInfo with its associated gameobject
		battleInfo.info.SendMessage ("onActorDeath", gameObject);
	
		//sends message to this entire hierarchy of gameobject
		SendMessage ("Death", SendMessageOptions.DontRequireReceiver);	//broadcast message
		
		
		//event notification
		if (Death != null) {
			Death ();
		}
	}
	
	
}

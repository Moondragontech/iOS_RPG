using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Actor : MonoBehaviour
{
	// basic attributes---------
	public float health = 100;			//current health of the player
	public float maxHealth = 100;		//max health a character can have
	//-------
	
	//events-----------
	public event EventNotification Death;	//event to notify on death
	
	//status
	public Slider healthSlider;			//to reference the slider component for Health
	
	//on Start
	void Start ()
	{
		postBeginPlay ();	//initialize for subclasses at start
	}
	
	//called per render update
	void Update ()
	{
		Tick (); 		//for updating subclasses
	}


	//basic methods-----------
	
	//for subclass initialize
	virtual public void postBeginPlay ()
	{
		//implement in the subclass
	}
	
	/// <summary>
	/// Tick this instance.
	/// </summary>
	virtual public void Tick ()
	{
		//implement in the subclass
	}
	
	/// <summary>
	/// Take damage, this method will deal damage to this actor/character
	/// </summary>
	/// <param name="inDamage">In damage.</param>
	/// <param name="damageDealer">Damage dealer.</param>
	/// <param name="Instigator">Instigator (damage dealer class, only if a character).</param>
	virtual public void takeDamage (float inDamage, GameObject damageDealer, Actor instigator = null)
	{
		//basic implementation
		//further implementation in the subclass may have armor and damage reduction features
		health = Mathf.Clamp (health - inDamage, 0, maxHealth);	//clamping the resulting value to go beyond 0
		
		//update the health slider, if presence
		updateHealthSlider ();
		
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
		battleController.info.SendMessage ("onActorDeath", gameObject);	//later this call will made to the current controller, after we create a GameInfo Class
	
		//sends message to this entire hierarchy of gameobject
		SendMessage ("Death", SendMessageOptions.DontRequireReceiver);	//broadcast message
		
		
		//event notification
		if (Death != null) {
			Death ();
		}
	}
	
	//utilities---
	//health slider
	protected void updateHealthSlider ()
	{
		if (healthSlider != null) {
			healthSlider.value = health;		//slider amount to that of health
		}
	}
	
	
}

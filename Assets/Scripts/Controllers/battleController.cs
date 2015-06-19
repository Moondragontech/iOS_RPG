using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

public class battleController : MonoBehaviour
{
//----
	public static battleController info;				//self reference for Class singleton, this class can only have 1 instance
//----
	public List<GameObject> Players;		//list of players
	public List<GameObject> Enemies;			//list of enemies
	
	public GameObject playerFlag;		//arrow to notify which character is active
	public GameObject enemyFlag;		//arrow to notify which character is active
	
	
	
	[Tooltip("base xp granted to the winner")]
	public int
		baseXPGranted = 20;	//base xp granted to the winner
	
	[Tooltip("base gold decreases upon death")]
	public int
		baseGoldPanaltyOnDeath = 30;	//base gold decreases upon death
	
	//every initial init
	void Awake ()
	{
		//initializing singleton
		info = this;
	}
	
/*	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}*/
	
	/// <summary>
	/// Fights the battle. called from the button mostly
	/// </summary>
	public void fightBattle ()	//multiple simultaneous call have been handled in character.cs using "DOTween.TotalPlayingTweens ()"
	{
		//start the fight sequence.... later it will be handled by player controller
		StartCoroutine (toFight ());
	}
	
	//fight Sequence
	IEnumerator toFight ()
	{
		
		int random = Random.Range (0, 2);		//local var to store a random
		
		//randomly choose which team will attack first
		switch (random) {
		case 0:
			{
				yield return StartCoroutine (allyAttack ());
				yield return StartCoroutine (enemyAttack ());
			}
			break;
		case 1:
			{
				yield return StartCoroutine (enemyAttack ());
				yield return StartCoroutine (allyAttack ());
			}
			break;
		}
		
		
		yield return null;
	}
	
	/// <summary>
	/// called from Actor class, when a associated character died
	/// </summary>
	/// <param name="inActor">In actor.</param>
	void onActorDeath (GameObject inActor)
	{
		Debug.Log ("Actor died ##: " + inActor.ToString ());
		
		//finds the inActor in the player/enemy list and removed it.
		Players.findAndRemove (inActor);
		Enemies.findAndRemove (inActor);
		
		//destroying the characters that are dead, we can use particle/extra effects later.
		//Destroy (inActor);		//obsolute - moved to per character subclass
		
	}
	
	/// <summary>
	/// Increases the exp on enemy kill.
	/// </summary>
	/// <param name="killedCharacterLevel">Killed character level.</param>
	public void increaseExpOnEnemyKill (int killedCharacterLevel)
	{
		dataAccess.info.updateXp ((uint)(killedCharacterLevel * baseXPGranted));	//with respect to enemy killed
	}
	
	/// <summary>
	/// Increases the gold on enemy kill.
	/// </summary>
	/// <param name="increaseGoldOnEnemyKill">Killed character level.</param>
	public void increaseGoldOnEnemyKill (int Amount)
	{
		dataAccess.info.updateGold (Amount);	//with respect to enemy killed
	}
	
	/// <summary>
	/// Decreases the gold when all player party members die
	/// </summary>
	/// <param name="decreaseGoldOnPlayerPartyDeath">Killed character level.</param>
	public void decreaseGoldOnPlayerPartyDeath (int characterLevel)
	{
		dataAccess.info.updateGold (-characterLevel * baseGoldPanaltyOnDeath);
	}
	
	//--fight sequence
	
	/// <summary>
	/// Allies attack.
	/// </summary>
	/// <returns>The attack.</returns>
	IEnumerator allyAttack ()
	{
		//first Players will attack
		foreach (var player in Players) {
			
			Tweener yieldTweener; //using for yielding untill a single fighting is done.
			
			if (Enemies.Count <= 0) 	//break it if there is no enemies
				break;
			
			player.GetComponent<Character> ().fight (Enemies [Random.Range (0, Enemies.Count)], out yieldTweener, 1.0f);	//fight sequence
			
			
			if (yieldTweener != null) {
				yield return yieldTweener.WaitForCompletion ();	//watiing for the fight to finish*
			}
			yield return null;
		}
	}
	
	/// <summary>
	/// Enemies attack.
	/// </summary>
	/// <returns>The attack.</returns>
	IEnumerator enemyAttack ()
	{
		//enemies fight second
		foreach (var enemy in Enemies) {
			
			Tweener yieldTweener; //using for yielding untill a single fighting is done.
			
			if (Players.Count <= 0) 	//break it if there is no enemies
				break;
			
			enemy.GetComponent<Character> ().fight (Players [Random.Range (0, Players.Count)], out yieldTweener, 1.0f);	//fight sequence
			
			if (yieldTweener != null) {
				yield return yieldTweener.WaitForCompletion ();	//watiing for the fight to finish
			}
			yield return null;
		}
	}
	
	
}

using UnityEngine;
using System.Collections;
using DG.Tweening;

/// <summary>
/// Main Character class, holding basic variable and methods of any character
/// </summary>
public class Character : Actor
{

	// basic attributes---------
	public float damage = 10;			//basic damage this character can give
	public float mana = 100;			//current mana of the player
	public float maxMana = 100;			//max mana a character can have
	//------
	
	
	/// <summary>
	/// Fight the specified target for fightDuration.
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="fightDuration">Fight duration.</param>
	virtual public void fight (GameObject target, out Tweener fightEndTweener, float fightDuration = 0.425f)
	{
		
		if (DOTween.TotalPlayingTweens () > 0) {	//if any tween is playing, dont play it again
			fightEndTweener = null;		//it will null as this whole function will be useless
			return;
		}
		
		onActivated (target, true);		//calling on activates as soon as the fignting starts
		gotoAndAttack (target, fightDuration, out fightEndTweener);		//basic fight action
	}
	
	/// <summary>
	/// Shake this instance and return the tweener
	/// </summary>
	void  shake (GameObject target, float duration=1, float strength=1, int vibrato = 5, float randomness = 10)
	{
		target.transform.DOShakePosition (duration, strength, vibrato, randomness)		//tween shake, returning the tweener
		.OnComplete (() => doDamage (damage, target));									//on complete, do the damage to the target
	}
	
	/// <summary>
	/// Goto and attack the traget for the fight duration
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="fightDuration">Fight duration.</param>
	void gotoAndAttack (GameObject target, float fightDuration, out Tweener fightEndTweener)
	{
		Sequence attackSequence = DOTween.Sequence ();	//initializing the sequence
		Vector3 targetPos = target.transform.position;
		Vector3 originalCharacterPos = transform.position;
		
		//safety for null reference
		if (target.GetComponent<Renderer> () == null || gameObject.GetComponent<SpriteRenderer> () == null) {
			fightEndTweener = null;		//it will null as this whole function will be useless
			return;
		}
		//creating the sequence
		attackSequence.Append (transform.DOMove (new Vector3 ((targetPos.x + (target.GetComponent<Renderer> ().bounds.extents.x * transform.localScale.x)), targetPos.y, targetPos.z), 0.5f)	//adjusting the character position with the bounds of the target's renderer
			.SetEase (Ease.InOutElastic)
			.OnComplete (() => shake (target, 1f, 0.2f, 7, 1)))		//Move to the target position and then shake the target
		.Join (gameObject.GetComponent<SpriteRenderer> ().DOFade (0f, 0.175f).SetDelay (0.15f))		//fade the active character to 0
		//.Append (shake (target, 1f, 0.2f, 7, 1))	//shake target
		.Append (gameObject.GetComponent<SpriteRenderer> ().DOFade (1, 0.5f));			//fade the active character to 1
		
		//deal damage to the target
		//doDamage (damage, target);
		
		//after the fighting sequence the character has to go to the original position
		fightEndTweener = backToIdle (originalCharacterPos, 0.5f, fightDuration);
	
	}
	
	/// <summary>
	/// Backs to idle pos.
	/// </summary>
	/// <param name="originalPos">Original position.</param>
	/// <param name="timeAfter">Time after.</param>
	Tweener backToIdle (Vector3 originalPos, float moveTime, float timeAfter)
	{
		//tween for moving to the original position
		return transform.DOMove (originalPos, moveTime).SetDelay (timeAfter)
		.OnComplete (() => onActivated (null, false));	//deactivating the infos
	}
	
	/// <summary>
	/// when this character is activated. 
	/// </summary>
	virtual public void onActivated (GameObject currentTarget = null, bool bActive =false)
	{
		//we may write some common feature here later
		
		//(test)-will make it more efficient later
		battleInfo.info.enemyFlag.SetActive (bActive);
		battleInfo.info.playerFlag.SetActive (bActive);
		
		//implement in the subclass
	}
	
	
}

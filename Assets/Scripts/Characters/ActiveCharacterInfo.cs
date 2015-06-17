using UnityEngine;
using System.Collections;

public class ActiveCharacterInfo : MonoBehaviour
{
	
	void Awake ()
	{
		gameObject.SetActive (false);
	}
	
	
	/// <summary>
	/// Sets activate arrow component active.
	/// </summary>
	/// <param name="target">Target.</param>
	public void setActivateArrow (Transform target)
	{
		
		Bounds CombineBounds = myUtilities.getTotalBounds (target);		//getting combined bounds for the hierarchy of "target"
		
		//Debug.Log ("combined : " + CombineBounds.extents.y);
		//Debug.Log ("Parent: " + target.GetComponent<Renderer> ().bounds.extents.y);
		
		if (CombineBounds.size != Vector3.zero) {	//if there are renders attached and has valid bounds
			transform.position = CombineBounds.center + new Vector3 (0, CombineBounds.extents.y);	//setting the position of the arrow with respect ot the target's rendering bound
		} else
			transform.position = target.position;		//simply setting the position
	}
}

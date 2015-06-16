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
		var tRenderer = target.GetComponent<Renderer> ();	//referencing the renderer
		
		if (tRenderer != null) {	//if there is valid renderer
			transform.position = tRenderer.bounds.center + new Vector3 (0, tRenderer.bounds.extents.y);	//setting the position of the arrow with respect ot the target's rendering bound
		} else
			transform.position = target.position;		//simply setting the position
	}
}

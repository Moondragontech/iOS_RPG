using UnityEngine;
using System.Collections;

public class getInputKey : MonoBehaviour
{
	//to store valid keycodes
	static private KeyCode[] validKeyCodes;

	// Use this for initialization
	void Start ()
	{
		if (validKeyCodes == null) {
			
			validKeyCodes = (KeyCode[])System.Enum.GetValues (typeof(KeyCode));
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.anyKeyDown) {	//if any key is down
			for (int i = 0; i < validKeyCodes.Length; i++) {	//iterate through every keycodes
				if (Input.GetKey (validKeyCodes [i])) {			//if that key is down
					Debug.Log ("input:: " + validKeyCodes [i].ToString ());	//debug
				}
			}
		}
	}
}

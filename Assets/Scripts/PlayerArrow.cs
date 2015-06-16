using UnityEngine;
using System.Collections;

public class PlayerArrow : MonoBehaviour
{

	public static Vector3 playerPosA;
	// Use this for initialization
	void Start ()
	{
		playerPosA = GameObject.Find ("FPlayer1").transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.transform.position = new Vector3 (playerPosA.x, playerPosA.y + 0.8F, playerPosA.z);
	}
}

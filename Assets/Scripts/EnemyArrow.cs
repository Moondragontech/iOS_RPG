using UnityEngine;
using System.Collections;

public class EnemyArrow : MonoBehaviour {
    public static Vector3 enemyPosA;
	// Use this for initialization
    void Start()
    {
        enemyPosA = GameObject.Find("Enemy1").transform.position;
	}
	
	// Update is called once per frame
	void Update () 
    {
            this.transform.position = new Vector3(enemyPosA.x, enemyPosA.y + 0.8F, enemyPosA.z);
	}
}

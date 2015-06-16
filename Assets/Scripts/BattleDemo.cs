using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BattleDemo : MonoBehaviour
{

	//defining a public variable meaning if that variable needed outside of class (do it only if neccesary)
	//OR if you need to excess it in the editor
	
	//codes should be well commented, make it a habbit

	public GameObject[] Players;
	public GameObject[] Enemys;
	public GameObject playerFlag;
	public GameObject enemyFlag;
	public Vector3[] playerPos;		
	public Vector3[] enemyPos;
	
	int i = 0;
	int j = 0;
	
	

	
	void Start ()
	{
		for (int i=0; i<3; i++) {
			playerPos [i] = Players [i].transform.position;
			enemyPos [i] = Enemys [i].transform.position;
		}
		
		
	}

	
	public void fight ()
	{
		StartCoroutine ("toFight");
		/*foreach(var player in Players)
		{
			Tweener tweener=player.transform.DOMove(enemyPos[i],1F);
			tweener.OnComplete(shakePosEnemy);
			i++;
		}
		foreach(var enemy in Enemys)
		{
			Tweener tweener=enemy.transform.DOMove(playerPos[j],1F);
			tweener.OnComplete(shakePosPlayer);
			j++;
		}*/
	}
	void shakePosEnemy ()
	{
		// debug messages should be always meaningful
		Debug.Log ("2");
		Tweener tweener = Enemys [i].transform.DOShakePosition (1F, 1F, 5, 10).OnComplete (backPosPlayer);
		Debug.Log ("3");
	}
	void shakePosPlayer ()
	{
		Tweener tweener = Players [j].transform.DOShakePosition (1F, 1F, 5, 10).OnComplete (backPosEnemy);
	}
	void backPosPlayer ()
	{
		Players [i].transform.position = playerPos [i];
	}
	void backPosEnemy ()
	{
		Enemys [j].transform.position = enemyPos [j];
	}
	
	//Rather than writting the whole fight sequence here, I would suggest, we should have a fight method in each character
	//and then just call the respective player and enemiy to fight
	//that will be much more modular and you can have a random effect.
	
	IEnumerator toFight ()
	{
		foreach (var player in Players) {

			int random = Random.Range (0, 3);
			PlayerArrow.playerPosA = player.transform.position;
			EnemyArrow.enemyPosA = Enemys [random].transform.position;
			Tweener tweener = player.transform.DOMove (new Vector3 (enemyPos [random].x - 0.7F, enemyPos [random].y, enemyPos [random].z), 0.5F);
			tweener.SetEase (Ease.InOutElastic);
			yield return new WaitForSeconds (0.1F);
			player.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0.75f);
			yield return new WaitForSeconds (0.025F);
			player.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0.5f);
			yield return new WaitForSeconds (0.025F);
			player.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0.25f);
			yield return new WaitForSeconds (0.025F);
			player.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0f);
			yield return new WaitForSeconds (0.025F);
			yield return new WaitForSeconds (0.1F);
			Tweener tweener1 = Enemys [random].transform.DOShakePosition (1F, 0.2F, 7, 1);
			player.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0.25f);
			yield return new WaitForSeconds (0.025F);
			player.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0.5f);
			yield return new WaitForSeconds (0.025F);
			player.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0.75f);
			yield return new WaitForSeconds (0.025F);
			player.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
			yield return new WaitForSeconds (0.025F);
			yield return new WaitForSeconds (0.4F);
			Tweener tweener2 = player.transform.DOMove (playerPos [i], 0.5F);
			i++;
			yield return new WaitForSeconds (0.5F);
			yield return null;
		}
		foreach (var enemy in Enemys) {
			int random = Random.Range (0, 3);
			EnemyArrow.enemyPosA = enemy.transform.position;
			PlayerArrow.playerPosA = Players [random].transform.position;
			Tweener tweener = enemy.transform.DOMove (new Vector3 (playerPos [random].x + 0.7F, playerPos [random].y, playerPos [random].z), 0.5F);
			tweener.SetEase (Ease.InOutElastic);
			yield return new WaitForSeconds (0.1F);
			enemy.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0.75f);
			yield return new WaitForSeconds (0.025F);
			enemy.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0.5f);
			yield return new WaitForSeconds (0.025F);
			enemy.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0.25f);
			yield return new WaitForSeconds (0.025F);
			enemy.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0f);
			yield return new WaitForSeconds (0.025F);
			yield return new WaitForSeconds (0.1F);
			Tweener tweener1 = Players [random].transform.DOShakePosition (1F, 0.2F, 7, 1);
			enemy.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0.25f);
			yield return new WaitForSeconds (0.025F);
			enemy.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0.5f);
			yield return new WaitForSeconds (0.025F);
			enemy.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0.75f);
			yield return new WaitForSeconds (0.025F);
			enemy.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
			yield return new WaitForSeconds (0.025F);
			yield return new WaitForSeconds (0.4F);
			Tweener tweener2 = enemy.transform.DOMove (enemyPos [j], 0.5F);
			j++;
			yield return new WaitForSeconds (0.5F);
			yield return null;
		}
		if (i >= 2) {
			i = 0;
		}
		if (j >= 2) {
			j = 0;
		}
	}
}

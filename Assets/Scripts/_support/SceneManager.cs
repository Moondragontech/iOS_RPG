using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour
{

	static int lastScene;
	static int currentScene = 0;
		
	
	/*void Awake ()
		{
				DontDestroyOnLoad (this.transform.gameObject);	// this let object to retain on level change/reload
		}*/
	
	//on scene change
	public static void ChangeScene (int sceneIndex)
	{
		lastScene = currentScene;
		currentScene = sceneIndex;
		//Debug.Log ("toScene : " + sceneIndex);
		Application.LoadLevel (sceneIndex);
				
		//Application.lo
	}
		
	//load last scene
	public void LoadLastScene ()
	{
		int last = lastScene;
		lastScene = currentScene;
		currentScene = last;
		Application.LoadLevel (currentScene);
	}
		
	//get the last scene that loaded
	public int getLastLoadedScene ()
	{
		return lastScene;
	}
		
	//reload level
	public void reloadLevel ()
	{
		//Debug.Log ("Reloading scene Index: " + Application.loadedLevel);
		ChangeScene (Application.loadedLevel);
				
	}
}

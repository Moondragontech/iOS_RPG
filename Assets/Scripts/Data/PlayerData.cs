// PlayerData
// This file is used to contain all the player data in a central location
// Will provide easy access to player data to other scripts
// Will also save and load player data
// The object that this is attached to will last even after new scene is laoded

using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerData : MonoBehaviour
{
	// Singleton pattern.
	// Ensures that there is only one of these and provides a global point of access
	static PlayerData mPlayerData;
	
	string saveFileName;
	
	// The current stats of the player. Add more stats below. 
	// Make them all private and provide access via properites to check for illegal data
	
	//private string mCharacterName; // Is it better to use a fixed array of characters ???
	[Range(0, 10000)]
	private uint
		mPartyGold;
	public uint partyGold { 
		get { 
			return mPartyGold; 
		} 
		set {  
			mPartyGold = (uint)Mathf.Clamp (((int)mPartyGold + (int)value), 0, 10000);	//->later 10000 should be replaced by a max variable
			Debug.Log ("gold changed to " + mPartyGold);
		} 
	} 
	
	[Range(0, 100)]
	private uint
		mPartyExperience;
	public uint partyExperience { 
		get { 
			return mPartyExperience; 
		} 
		set {  
			mPartyExperience = value;
			Debug.Log ("xp changed to " + mPartyExperience);
		} 
	} 
	
	[Range(0, 100)]
	private uint
		mPartyLevel;
	public uint partyLevel { 
		get { 
			return mPartyLevel; 
		} 
		set {  
			mPartyLevel = value;
			Debug.Log ("Level changed to " + mPartyLevel);
		} 
	} 
	
	void Awake ()
	{
		// making sure that only of these exist and it is not lost when new scene is loaded
		if (mPlayerData == null) {
			DontDestroyOnLoad (gameObject);
			mPlayerData = this;
		} else if (mPlayerData != this) {
			Destroy (gameObject);
		}
	}
	
	// Use this for initialization
	void Start ()
	{
		saveFileName = Application.persistentDataPath + "/playerInfo.dat";
		Debug.Log ("saveFileName = " + saveFileName);
	}
	
	// Use this to access the data in other scripts
	public static PlayerData GetInstance ()
	{
		return mPlayerData;
	}
	
	// This will work on every platform except web
	// Need to check if additional work is required for ios devices
	public void Save ()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (saveFileName);
		
		SaveData data = new SaveData ();
		
		data.mPartyGold = mPartyGold;
		data.mPartyExperience = mPartyExperience;
		data.mPartyLevel = mPartyLevel;
		
		bf.Serialize (file, data); // Can also serialize to string and send over the web or write to playerprefs
		file.Close ();
		
		Debug.Log ("Save successful");
	}
	
	public void Load ()
	{
		if (File.Exists (saveFileName)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (saveFileName, FileMode.Open);
			SaveData data = (SaveData)bf.Deserialize (file);
			file.Close ();
			
			mPartyGold = data.mPartyGold;
			mPartyExperience = data.mPartyExperience;
			mPartyLevel = data.mPartyLevel;
			
			Debug.Log ("Load successful");
		} else {
			Debug.LogError ("File does not exist");
		}
	}
}

// All the data that can be saved
[Serializable]
class SaveData
{
	public uint mPartyGold;
	public uint mPartyExperience;
	public uint mPartyLevel;
}

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
	
	//public string mCharacterName; // Is it better to use a fixed array of characters ???
	[Range(0, 10000)]public uint mPartyGold;
	
	[Range(0, 100)]public uint mPlayer1CurrentHealth;
	[Range(0, 100)]public uint mPlayer1CurrentMana;
	[Range(0, 100)]public uint mPlayer1CurrentExperience;
	[Range(0, 100)]public uint mPlayer1Level;
	
	[Range(0, 100)]public uint mPlayer2CurrentHealth;
	[Range(0, 100)]public uint mPlayer2CurrentMana;
	[Range(0, 100)]public uint mPlayer2CurrentExperience;
	[Range(0, 100)]public uint mPlayer2Level;
	
	[Range(0, 100)]public uint mPlayer3CurrentHealth;
	[Range(0, 100)]public uint mPlayer3CurrentMana;
	[Range(0, 100)]public uint mPlayer3CurrentExperience;
	[Range(0, 100)]public uint mPlayer3Level;


	void Awake()
	{
		// making sure that only of these exist and it is not lost when new scene is loaded
		if(mPlayerData == null)
		{
			DontDestroyOnLoad(gameObject);
			mPlayerData = this;
		}
		else if(mPlayerData != this)
		{
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start()
	{
		saveFileName = Application.persistentDataPath + "/playerInfo.dat";
		Debug.Log ("saveFileName = " + saveFileName);
	}

	// Use this to access the data in other scripts
	public static PlayerData GetInstance()
	{
		return mPlayerData;
	}

	// This will work on every platform except web
	// Need to check if additional work is required for ios devices
	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(saveFileName);

		SaveData data = new SaveData();

		data.mPartyGold = mPartyGold;

		data.mPlayer1CurrentHealth = mPlayer1CurrentHealth;
		data.mPlayer1CurrentMana = mPlayer1CurrentMana;
		data.mPlayer1CurrentExperience = mPlayer1CurrentExperience;
		data.mPlayer1Level = mPlayer1Level;
		
		data.mPlayer2CurrentHealth = mPlayer2CurrentHealth;
		data.mPlayer2CurrentMana = mPlayer2CurrentMana;
		data.mPlayer2CurrentExperience = mPlayer2CurrentExperience;
		data.mPlayer2Level = mPlayer2Level;
		
		data.mPlayer3CurrentHealth = mPlayer3CurrentHealth;
		data.mPlayer3CurrentMana = mPlayer3CurrentMana;
		data.mPlayer3CurrentExperience = mPlayer3CurrentExperience;
		data.mPlayer3Level = mPlayer3Level;

		bf.Serialize(file, data); // Can also serialize to string and send over the web or write to playerprefs
		file.Close();

		Debug.Log ("Save successful");
	}

	public void Load()
	{
		if(File.Exists(saveFileName))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(saveFileName, FileMode.Open);
			SaveData data = (SaveData)bf.Deserialize(file);
			file.Close();

			mPartyGold = data.mPartyGold;
			
			mPlayer1CurrentHealth = data.mPlayer1CurrentHealth;
			mPlayer1CurrentMana = data.mPlayer1CurrentMana;
			mPlayer1CurrentExperience = data.mPlayer1CurrentExperience;
			mPlayer1Level = data.mPlayer1Level;
			
			mPlayer2CurrentHealth = data.mPlayer2CurrentHealth;
			mPlayer2CurrentMana = data.mPlayer2CurrentMana;
			mPlayer2CurrentExperience = data.mPlayer2CurrentExperience;
			mPlayer2Level = data.mPlayer2Level;
			
			mPlayer3CurrentHealth = data.mPlayer3CurrentHealth;
			mPlayer3CurrentMana = data.mPlayer3CurrentMana;
			mPlayer3CurrentExperience = data.mPlayer3CurrentExperience;
			mPlayer3Level = data.mPlayer3Level;

			Debug.Log ("Load successful");
		}
		else
		{
			Debug.LogError ("File does not exist");
		}
	}
}

// All the data that can be saved
[Serializable]
class SaveData
{
	public uint mPartyGold;

	public uint mPlayer1CurrentHealth;
	public uint mPlayer1CurrentMana;
	public uint mPlayer1CurrentExperience;
	public uint mPlayer1Level;

	public uint mPlayer2CurrentHealth;
	public uint mPlayer2CurrentMana;
	public uint mPlayer2CurrentExperience;
	public uint mPlayer2Level;

	public uint mPlayer3CurrentHealth;
	public uint mPlayer3CurrentMana;
	public uint mPlayer3CurrentExperience;
	public uint mPlayer3Level;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadButton : MonoBehaviour {

    // Use this for initialization
    // Attach to buttons to call the save and load functions
    // Mainly used for testing not for actual gameplay
    public GameObject Player;
	void Start () {
		
	}
	
	public void Save()
    {
        Debug.Log("Saving Game");
        PlayerData dataToSave = new PlayerData();
        dataToSave.currentPosition = Player.transform.position;
        dataToSave.currentInventory = Player.GetComponent<Inventory>();
        SaveLoad.SaveGame(dataToSave, "player.json");
        Debug.Log(dataToSave.currentPosition);
    }
    public void Load()
    {
        Debug.Log("Loading Game");
        PlayerData dataToLoad = new PlayerData();
        SaveLoad.LoadGame("player.json");
        dataToLoad = SaveLoad.loadData;
        Player.transform.position = dataToLoad.currentPosition;
        Debug.Log(SaveLoad.loadData.currentPosition);
    }
    public void Delete()
    {
        Debug.Log("Deleting Game");
        SaveLoad.DeleteSave("player.json");
    }
}

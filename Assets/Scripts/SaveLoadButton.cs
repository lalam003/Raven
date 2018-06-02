using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadButton : MonoBehaviour {

    // Use this for initialization
    // Attach to buttons to call the save and load functions
    // Mainly used for testing not for actual gameplay
    // Store player information in PlayerData class
    // Store inventory information in InventoryData class.

    private GameObject Player;
	void Start ()
    {
        Player = Blackboard.Player.gameObject;
	}
	//Disclaimer I promise nothing
	public void Save()
    {
        Debug.Log("Saving Game");
        PlayerData.currentPlayer.currentPosition = Player.transform.position;
        Inventory tempInv = Player.GetComponent<Inventory>();
        foreach(KeyValuePair<Item, uint> item in tempInv.Items)
        {
            PlayerData.currentPlayer.items.Add(item.Key.ItemKey);
            PlayerData.currentPlayer.amount.Add(item.Value);
        }
        SaveLoad.SaveGame(PlayerData.currentPlayer, "player.json");
        Debug.Log(PlayerData.currentPlayer.currentPosition);
    }
    //All scriptable objects should be in Resources Folder and name is the same as the itemkey
    public void Load()
    {
        Debug.Log("Loading Game");
        SaveLoad.LoadGame("player.json");
        PlayerData.currentPlayer = SaveLoad.loadData;
        Player.transform.position = PlayerData.currentPlayer.currentPosition;
        for(int i =0; i < PlayerData.currentPlayer.items.Count; i++)
        {
            Item item = Resources.Load<Item>(PlayerData.currentPlayer.items[i]);
            print("item: " + item.name);
            Player.GetComponent<Inventory>().AddItem(item, PlayerData.currentPlayer.amount[i]);
        }
        Debug.Log(SaveLoad.loadData.currentPosition);
        Blackboard.Title.closeMenu();
    }
    public void Delete()
    {
        Debug.Log("Deleting Game");
        SaveLoad.DeleteSave("player.json");
    }
}

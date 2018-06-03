using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadButton : MonoBehaviour {

    // Use this for initialization
    // Attach to buttons to call the save and load functions
    // Mainly used for testing not for actual gameplay
    // Store player information in PlayerData class
    // Store inventory information in InventoryData class.
    
	public void Save()
    {
        Debug.Log("Saving Game");
        PlayerData.currentPlayer.currentPosition = Blackboard.Player.transform.position;
        Inventory tempInv = Blackboard.Player.GetComponent<Inventory>();
        PlayerData.currentPlayer.items.Clear();
        PlayerData.currentPlayer.amount.Clear();
        foreach (KeyValuePair<Item, uint> item in tempInv.Items)
        {
            Debug.Log("Saved item: " + item.Key);
            PlayerData.currentPlayer.items.Add(item.Key.ItemKey);
            PlayerData.currentPlayer.amount.Add(item.Value);
        }
        SaveLoad.SaveGame(PlayerData.currentPlayer, "player.json");
        Debug.Log(PlayerData.currentPlayer.currentPosition);
    }
    //All scriptable objects should be in Resources Folder and name is the same as the itemkey
    public bool Load()
    {
        Debug.Log("Loading Game");
        SaveLoad.LoadGame("player.json");
        PlayerData.currentPlayer = SaveLoad.loadData;
        if (PlayerData.currentPlayer.currentPosition == null)
        {
            return false;
        }
        Blackboard.Player.transform.position = PlayerData.currentPlayer.currentPosition;
        Debug.Log("loaded: " + SaveLoad.loadData.currentPosition);
        for (int i =0; i < PlayerData.currentPlayer.items.Count; i++)
        {
            Item item = Resources.Load<Item>(PlayerData.currentPlayer.items[i]);
            Blackboard.Player.GetComponent<Inventory>().AddItem(item, PlayerData.currentPlayer.amount[i]);
            Debug.Log("loaded: " + item.name);
        }
        return true;
    }
    public void Delete()
    {
        Debug.Log("Deleting Game");
        SaveLoad.DeleteSave("player.json");
    }
}
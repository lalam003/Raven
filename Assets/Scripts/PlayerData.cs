using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    [SerializeField]
    public static PlayerData currentPlayer; // set this variable to player instance whenver created or loaded.
    public Vector3 currentPosition;
    public List<string> eventList;
    public List<bool> eventHasOccurred;
    public List<string> items;
    public List<uint> amount;
    //Itemdata
    //Events
    //each event needs an id and stores itself in a static 
    //class which stores its id and event task to trigger
    //in addition to the task it will set another callback that will tell the class the event was triggered.
    public PlayerData()
    {
        currentPosition = new Vector3();
        eventList = new List<string>();
        eventHasOccurred = new List<bool>();
        items = new List<string>();
        amount = new List<uint>();
    }
}

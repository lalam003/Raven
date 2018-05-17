using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    [SerializeField]
    public static PlayerData currentPlayer;
    public Vector3 currentPosition;
    [SerializeField]
    public Inventory currentInventory;
    //Itemdata
    //Events
    //each event needs an id and stores itself in a static 
    //class which stores its id and event task to trigger
    //in addition to the task it will set another callback that will tell the class the event was triggered.
    public PlayerData()
    {

    }
}

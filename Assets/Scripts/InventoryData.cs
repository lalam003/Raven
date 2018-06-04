using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{

    public static Inventory currentInventory;
    public Dictionary<Item, uint> Items;

    public InventoryData()
    {

    }
}

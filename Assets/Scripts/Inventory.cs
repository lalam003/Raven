﻿using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // TODO: Remove this test later
    public Item WaterBottle;
    public Item Note1;
    private void Awake()
    {
        AddItem(WaterBottle, 2);
        AddItem(Note1, 1);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) { items.Remove(WaterBottle); }
    }
    // End Test

    [SerializeField]
    private uint maxSize = 20;

    private Dictionary<Item, uint> items = new Dictionary<Item, uint>();
    public Dictionary<Item, uint> Items
    {
        get
        {
            return items;
        }
    }

    public bool AddItem(Item new_item, uint count = 1)
    {
        if (!new_item)
        {
            Debug.LogWarning("Attemptingn to add a null item.");
        }   

        if (count > new_item.MaxAmount)
        {
            Debug.LogWarning("You are attempting to add "+count+" items but the max allowed amount is "+new_item.MaxAmount);
            return false;
        }
        if (!items.ContainsKey(new_item) && items.Count < maxSize)
        {
            // add new item
            items.Add(new_item, count);
            return true;
        }
        else if (!items.ContainsKey(new_item) && (items[new_item] + count) <= new_item.MaxAmount)
        {
            // increase qty
            items[new_item] += count;
            return true;
        }
        return false;
    }
    
    public bool UseItem(Item itemToUse)
    {
        if(Items.ContainsKey(itemToUse))
        {
            Items[itemToUse]--;

            if(Items[itemToUse] == 0)
            {
                Items.Remove(itemToUse);
                return true;
            }
        }

        return false;
    }
}
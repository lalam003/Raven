using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteractable : Interactable
{
    [SerializeField]
    private Item itemToPickup;
    [SerializeField]
    private uint countToPickup;
    private bool completed = false;

    public override void Interact()
    {
        if(completed == false)
        {
            Blackboard.Player.Inventory.AddItem(itemToPickup, countToPickup);
            OnInteract();
            completed = true;
            gameObject.SetActive(false);
        }
    }
}

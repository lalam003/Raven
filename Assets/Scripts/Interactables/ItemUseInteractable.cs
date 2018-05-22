using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseInteractable : Interactable
{
    [SerializeField]
    private Item itemToCheck;
    private bool completed = false;

    public override void Interact()
    {
        if (completed == false)
        {
            if (Blackboard.Player.Inventory.UseItem(itemToCheck))
            {
                OnInteract();
                completed = true;
            }
        }
    }
}

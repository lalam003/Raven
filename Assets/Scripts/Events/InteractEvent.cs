using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class InteractEvent : Event
{
    [SerializeField]
    private bool occurOnce = true;
    private Interactable interactable;
    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.OnInteract += task.ExecuteTask;
        if(occurOnce)
        {
            interactable.OnInteract += () => { interactable.OnInteract -= task.ExecuteTask; PlayerData.currentPlayer.eventList.Add(task.name); PlayerData.currentPlayer.eventHasOccurred.Add(true);};
        }
    }
}

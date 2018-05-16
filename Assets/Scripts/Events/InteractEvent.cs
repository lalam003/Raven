using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class InteractEvent : Event
{
    private Interactable interactable;
    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.OnInteract += task.ExecuteTask;
    }
}

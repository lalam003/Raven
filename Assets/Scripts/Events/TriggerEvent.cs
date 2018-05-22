using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Trigger))]
public class TriggerEvent : Event
{
    private Trigger trigger;
    private void Awake()
    {
        trigger = GetComponent<Trigger>();
        trigger.OnTrigger += task.ExecuteTask;
        trigger.OnTrigger += () => { trigger.OnTrigger -= task.ExecuteTask; };
    }
}

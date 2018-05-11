using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimeSystem))]
public class TimeEvent : Event
{
    [SerializeField]
    private int timeToTrigger;

    private void Awake()
    {
        TimeSystem.timeEvents.Add(timeToTrigger, task.ExecuteTask);
    }
}

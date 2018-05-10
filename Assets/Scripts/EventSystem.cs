using UnityEngine;
using System.Collections.Generic;

public class EventSystem : MonoBehaviour
{
    [SerializeField]
    private List<Event> events;

    private void Awake()
    {
        //  events = new List<Event>();
    }
    public void TriggerEvent(int index)
    {
        if(index >= events.Count || index < 0)
        {
            Debug.Log("Index Out of Range");
            return;
        }
        else
        {
            events[index].TriggerTask();
        }
    }
}

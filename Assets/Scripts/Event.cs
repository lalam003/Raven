using UnityEngine;

public class Event : ScriptableObject
{
    public enum EventType
    {
        Time = 0,
        Interact,
        Trigger
    }

    [SerializeField]
    private EventType type;

    [SerializeField]
    private EventTask task;
}

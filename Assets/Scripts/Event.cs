using UnityEngine;

class Event : MonoBehaviour
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
    private int timeToTrigger;

    [SerializeField]
    private Interactable interactable;

    [SerializeField]
    private Trigger trigger;

    [SerializeField]
    private EventTask task;

    public void TriggerTask()
    {
        task.ExecuteTask();
    }
}
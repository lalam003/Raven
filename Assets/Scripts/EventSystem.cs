using UnityEngine;
using System.Collections.Generic;

public class EventSystem : MonoBehaviour
{
    [SerializeField]
    private List<Event> events;

    private void Awake()
    {
        events = new List<Event>();
    }
}

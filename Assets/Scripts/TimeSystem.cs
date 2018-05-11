using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    private static int timeElapsed;

    public static Dictionary<int, Action> timeEvents;

    public static void IncrementTime()
    {
        timeElapsed++;
        if(timeEvents.ContainsKey(timeElapsed))
        {
            timeEvents[timeElapsed].Invoke();
        }
    }
}

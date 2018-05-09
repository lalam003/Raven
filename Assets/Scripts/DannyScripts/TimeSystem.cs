using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSystem  {

    private static int timeElapsed = 0;
    delegate void OnInteract();
    public void IncrementTime(int timeAdvanced)
    {
        timeElapsed += timeAdvanced;
    }

    public int GetTime()
    {
        return timeElapsed;
    }
}

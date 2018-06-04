using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOnNightEvent : EventTask
{
    public override void ExecuteTask()
    {
        if(TimeSystem.isNight)
        {
            Blackboard.Player.PlayerDeath();
        }
    }
}

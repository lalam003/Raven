using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : EventTask
{
    private bool on = false;
    public bool On
    {
        get
        {
            return on;
        }
    }

    public override void ExecuteTask()
    {
        on = !on;
    }
}

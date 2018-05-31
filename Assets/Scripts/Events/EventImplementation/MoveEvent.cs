using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEvent : EventTask
{
    [SerializeField]
    Transform positionToMoveTo;

    public override void ExecuteTask()
    {
        gameObject.transform.position = positionToMoveTo.position;
    }
}

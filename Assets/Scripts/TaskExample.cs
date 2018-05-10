using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TaskExample : EventTask {


    public override void ExecuteTask()
    {
        //throw new System.NotImplementedException();
        Debug.Log("Doing Some Task");
        this.GetComponent<Text>().text = "Event Triggered";
    }
}

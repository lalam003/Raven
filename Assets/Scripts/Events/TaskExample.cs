using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TaskExample : EventTask {


    public override void ExecuteTask()
    {
        //throw new System.NotImplementedException();
        Debug.Log("Doing Some Task");
        Debug.Log(gameObject.name);
        this.GetComponent<Text>().text = "Event Triggered";
    }
}

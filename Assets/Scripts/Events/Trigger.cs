using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private Callback onTrigger;
    public Callback OnTrigger
    {
        get
        {
            return onTrigger;
        }
        set
        {
            if(value != null)
            {
                onTrigger = value;
            }
            else
            {
                onTrigger = () => { };
            }
        }
    }
}

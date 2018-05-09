﻿using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    private Callback onInteract;
    public Callback OnInteract
    {
        get
        {
            return onInteract;
        }
        set
        {
            if(value != null)
            {
                onInteract = value;
            }
            else
            {
                onInteract = () => { };
            }
        }
    }

    public abstract void Interact();
}

public delegate void Callback();
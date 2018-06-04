using UnityEngine;

public class Interactable : MonoBehaviour
{
    private Callback onInteract;
    public Callback OnInteract
    {
        get
        {
            if(onInteract == null)
            {
                onInteract = () => { };
            }

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

    public virtual void Interact()
    {
        OnInteract();
    }

    protected void Awake()
    {
        OnInteract += TimeSystem.IncrementTime;
    }
}

public delegate void Callback();
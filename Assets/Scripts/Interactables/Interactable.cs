using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    private static Callback onInteract;
    public static Callback OnInteract
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
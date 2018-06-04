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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTrigger();
    }
}

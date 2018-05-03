using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingCircle : Interactable
{
    private bool blue = true;

    public override void Interact()
    {
        GetComponent<SpriteRenderer>().color = (blue) ? Color.red : Color.blue;
        blue = !blue;
    }
}

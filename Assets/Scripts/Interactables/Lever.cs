using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : EventTask
{
    [SerializeField]
    private Sprite onImage, offImage;
    private SpriteRenderer spriteRenderer;
    private bool on = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

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
        if(on)
        {
            spriteRenderer.sprite = onImage;
        }
        else
        {
            spriteRenderer.sprite = offImage;
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueSystem))]
public class TalkingInteractable : Interactable
{
    private DialogueSystem dialogueSystem;
    protected const string fileName = "BaseJson.json";
    protected const string keyName = "dialogue";
    TimeSystem t;
    private void Awake()
    {
        t = new TimeSystem();
        dialogueSystem = GetComponentInChildren<DialogueSystem>();
    }

    public override void Interact()
    {
        if (dialogueSystem.DialogueRunning)
        {
            dialogueSystem.BreakDialogue();
        }
        else
        {
            dialogueSystem.DisplayText(fileName, keyName);
            t.IncrementTime(1);
        }
        
    }
}

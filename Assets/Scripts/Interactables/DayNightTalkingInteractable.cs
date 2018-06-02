using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueSystem))]
public class DayNightTalkingInteractable : Interactable
{
    private DialogueSystem dialogueSystem;

    [SerializeField]
    protected string fileName;
    [SerializeField]
    protected string dayKeyName = "day_dialogue";
    [SerializeField]
    protected string nightKeyName = "night_dialogue";

    private void Awake()
    {
        dialogueSystem = GetComponentInChildren<DialogueSystem>();
    }

    public override void Interact()
    {
        if (dialogueSystem.DialogueRunning)
        {
            bool finished = !dialogueSystem.BreakDialogue();
            
            if(finished)
            {
                OnInteract();
            }
        }
        else
        {
            if(TimeSystem.isNight)
            {
                dialogueSystem.DisplayText(fileName, dayKeyName);
            }
            else
            {
                dialogueSystem.DisplayText(fileName, nightKeyName);
            }
        }
    }
}

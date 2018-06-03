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
            dialogueSystem.BreakDialogue();
        }
        else
        {
            if(TimeSystem.isNight)
            {
                dialogueSystem.DisplayText(fileName, dayKeyName, () => { OnInteract(); });
            }
            else
            {
                dialogueSystem.DisplayText(fileName, nightKeyName, () => { OnInteract(); });
            }
        }
    }
}

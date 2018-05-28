using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathInteractable : Interactable
{
    private DialogueSystem dialogueSystem;
    private bool doneTalking = false;
    protected const string fileName = "Die.json";
    protected const string keyName = "dialogue";

    private void Awake()
    {
        dialogueSystem = GetComponent<DialogueSystem>();

    }

    public override void Interact()
    {
        if (dialogueSystem.DialogueRunning)
        {
            dialogueSystem.BreakDialogue();
        }
        else
        {
            dialogueSystem.DisplayText(fileName, keyName, Blackboard.Player.PlayerDeath);
        }
    }
}

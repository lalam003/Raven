using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathInteractable : Interactable
{
    private DialogueSystem dialogueSystem;

    [SerializeField]
    protected string fileName;
    [SerializeField]
    protected string keyName;

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
            dialogueSystem.DisplayText(
                fileName, 
                keyName, 
                () => 
                {
                    Blackboard.Player.PlayerDeath();
                    OnInteract();
                });
        }
    }
}

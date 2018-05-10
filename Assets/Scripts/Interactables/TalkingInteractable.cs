using UnityEngine;

[RequireComponent(typeof(DialogueSystem))]
public class TalkingInteractable : Interactable
{
    private DialogueSystem dialogueSystem;
    protected const string fileName = "BaseJson.json";
    protected const string keyName = "dialogue";

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
            dialogueSystem.DisplayText(fileName, keyName);
            ESys.TriggerEvent(0);
        }
    }
}

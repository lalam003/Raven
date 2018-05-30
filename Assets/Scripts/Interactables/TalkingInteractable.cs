using UnityEngine;

[RequireComponent(typeof(DialogueSystem))]
public class TalkingInteractable : Interactable
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
            dialogueSystem.DisplayText(fileName, keyName, () => { OnInteract(); });
        }
    }
}

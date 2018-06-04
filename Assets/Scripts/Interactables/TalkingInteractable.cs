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
            Animator anim = GetComponent<Animator>();
            if(anim != null)
            {
                if(Blackboard.Player.PlayerMovement.playerFacingDirection == PlayerMovement.Direction.West)
                {
                    anim.SetBool("FaceLeft", false);
                }
                else if (Blackboard.Player.PlayerMovement.playerFacingDirection == PlayerMovement.Direction.East)
                {
                    anim.SetBool("FaceLeft", true);
                }
            }
        }
    }
}

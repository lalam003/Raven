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
        base.Awake();
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

            Animator anim = GetComponent<Animator>();
            if (anim != null)
            {
                if (Blackboard.Player.PlayerMovement.playerFacingDirection == PlayerMovement.Direction.West)
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

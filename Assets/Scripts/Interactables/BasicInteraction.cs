using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicInteraction : Interactable
{


    // Use this for initialization
    [SerializeField]
    private DialogueSystem DialogueSys;
    private void Awake()
    {
        DialogueSys = GetComponentInChildren<DialogueSystem>();
    }
    public void SetActive(bool value)
    {
    }
    public override void Interact()
    {
        Debug.Log("Interacting");
        
        DialogueSys.DisplayText("BaseJson.json", "dialogue");
        
        //DialogueBox.SetActive(false);
    }	

}

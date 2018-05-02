using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{

    /* Dialogue System attached to interactable objects such as NPCS or anything that need a text box.
     * OnTriggerStay will only allow interaction if player is within range of object.
     * Needs testing with overlapping triggers.
     */
    private bool isInteracting = false;
    [SerializeField]
    private GameObject DialogueBox;
    [SerializeField]
    private string JsonFile, Subsection;


    private void Start()
    {
        if (DialogueBox == null)
        {
            Debug.LogError("DialogueBox Missing");
        }
    }



    // Update is called once per frame
    private void Update ()
    {
		if(DialogueBox.activeSelf == false)
        {
            isInteracting = false;
        }
	}
    //Returns current state of interaction
    public bool GetStatus()
    {
        return isInteracting;
    }
    //Specifies which Json and which section to read from
    private void InjectDialogue()
    {
        ReadJson dialogue = DialogueBox.GetComponent<ReadJson>();
        if(dialogue == null)
        {
            Debug.Log("Failed to get Json data");

            return;
        }
        else
        {
            //Can be called with no arguments but will default to BaseJson.json
            dialogue.SetParameters(JsonFile, Subsection);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(DialogueBox != null)
                {
                    if(DialogueBox.activeSelf == false)
                    {
                        InjectDialogue();
                        DialogueBox.SetActive(true);
                        isInteracting = true;
                    }
                    else
                    {
                        Debug.Log("Currently Interacting with another Object");
                    }
                }
            }
        }
    }
}

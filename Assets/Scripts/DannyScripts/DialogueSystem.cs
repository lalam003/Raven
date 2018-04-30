using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour {

    /* Dialogue System attached to interactable objects such as NPCS or anything that need a text box.
     * OnTriggerStay will only allow interaction if player is within range of object. 
     */
    bool isInteracting = false;
    [SerializeField]
    GameObject DialogueBox;
    [SerializeField]
    string JsonFile, Subsection;
	void Start () {
        //Search by tag or name or some kind of lookup if using serialize field to get a reference isnt to your taste
		//DialogueBox = GameObject.Find()
	}
	
	// Update is called once per frame
	void Update () {
		if(DialogueBox.activeSelf == false)
        {
            isInteracting = false;
        }
	}
    void InjectDialogue()
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

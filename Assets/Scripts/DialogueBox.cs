using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    private Text dialogueText;

    private void Awake()
    {
        dialogueText = GetComponentInChildren<Text>();
        
        if(dialogueText == null)
        {
            Debug.LogError("Error No DialogueText detected, creating new Text");
            GameObject obj = new GameObject("DialogueText");
            obj.transform.SetParent(transform);
            obj.AddComponent<RectTransform>();
            obj.AddComponent<CanvasRenderer>();
            obj.AddComponent<Text>();
            dialogueText = obj.GetComponent<Text>();
        }

        gameObject.SetActive(false);
    }

    public void SetText(string text)
    {
        dialogueText.text = text;
    }
}

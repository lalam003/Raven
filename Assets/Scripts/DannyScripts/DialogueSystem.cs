using System.Collections;
using UnityEngine;
using LightJson;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    private const string filePath = "/Resources/";

    private JsonArray dialogueText;
    private IEnumerator routine;
    private string currentLine;
    private bool continueDisplay = false;

    [SerializeField]
    private DialogueBox dialogueBox;
    //Text speed in letters per second
    [SerializeField]
    private float textSpeed;

    private bool dialogueRunning = false;
    public bool DialogueRunning
    {
        get
        {
            return dialogueRunning;
        }
    }

    private void Awake()
    {
        if (dialogueBox == null)
        {
            Debug.LogError("DialogueBox Missing");
        }
    }

    public void DisplayText(string filename, string key)
    {
        JsonObject file = ReadJson.ParseJsonFile(Application.dataPath + filePath + filename);

        if(file.ContainsKey(key))
        {
            dialogueText = file[key].AsJsonArray;
            StartCoroutine(runDialogue());
        }
        else
        {
            Debug.LogError(filename + " does not contain valid keyname " + key);
        }
    }

    public void SetTextSpeed(float lettersPerSecond)
    {
        textSpeed = lettersPerSecond;
    }

    public bool BreakDialogue()
    {
        if(routine.MoveNext())
        {
            StopCoroutine(routine);
            dialogueBox.SetText(currentLine);

            return true;
        }

        continueDisplay = false;
        return false;
    }

    private IEnumerator runDialogue()
    {
        dialogueBox.gameObject.SetActive(true);
        dialogueRunning = true;
        yield return null;

        foreach(JsonValue value in dialogueText)
        {
            currentLine = value;
            routine = printText(currentLine);
            StartCoroutine(routine);
            continueDisplay = true;
            while (routine.MoveNext())
            {
                yield return null;
            }
            while(continueDisplay)
            {
                yield return null;
            }
        }

        dialogueRunning = false;
        dialogueBox.gameObject.SetActive(false);
    }

    private IEnumerator printText(string text)
    {
        yield return null;

        WaitForSeconds waitTime = new WaitForSeconds(1.0f / textSpeed);
        string output = "";
        foreach (char letter in text)
        {
            output += letter;
            dialogueBox.SetText(output);
            yield return waitTime;

        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using LightJson;
using LightJson.Serialization;
using System.Reflection;
//Author Danny Diep
public class ReadJson : MonoBehaviour
{

    /* Attach this script to a Text UI object
     * Specify the text to be loaded in the fileToRead field 
     * Specify how fast text is printed in textSpeed field
     * Fastest speed is 0.01f, slow is 0.3f adjust to taste
     * Files are currently read from Resources folder in Unity Editor
     * 
     */


    // Use this for initialization
    private JsonObject jsonFile;
    private Text t;
    private JsonArray values;
    private int next = 0;
    [SerializeField]
    public string fileToRead = "BaseJson.json";
    [SerializeField]
    private string subsection = "dialogue";
    [SerializeField]
    private float textSpeed = 0.01f;
    private bool coroutineRunning = false;

    private void Start ()
    {
        jsonFile = new JsonObject();
        string datapath = Application.dataPath + "/Resources/" + fileToRead;
        jsonFile = JsonReader.ParseFile(datapath);
        t = this.GetComponentInChildren<Text>();
        values = JsonValue.Parse(jsonFile.ToString())[subsection].AsJsonArray;
        ScrollText();
    }
    public void Reset()
    {
        t.text = "";
        next = 0;
    }
    public void SetTextSpeed(float lettersPerSecond)
    {
        textSpeed = lettersPerSecond;
    }
    public void SetParameters(string newFile = "BaseJson.json", string newSubsection = "dialogue")
    {
        string datapath = Application.dataPath + "/Resources/" + newFile;
        jsonFile = JsonReader.ParseFile(datapath);
        values = JsonValue.Parse(jsonFile.ToString())[subsection].AsJsonArray;
    }
    private void OnEnable()
    {
        Reset();
        ScrollText();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(coroutineRunning == false)
            {
                ScrollText();
            }
        }
    }
    private IEnumerator PrintText(int index)
    {
        yield return null;

        string sentence = values[index];
        string output = "";
        foreach (char word in sentence )
        {
            output += word;
            if(Input.GetKeyDown(KeyCode.Space))
            {
                t.text = sentence;
                coroutineRunning = false;

                yield break;
            }
            else
            {
                yield return new WaitForSeconds(textSpeed);
                t.text = output;
            }
                     
        }
        coroutineRunning = false;
    }
    
    private void ScrollText()
    {
        if(next >= values.Count)
        {
            Reset();
            this.gameObject.SetActive(false);

            return;
        }
        else
        {
            coroutineRunning = true;
            StartCoroutine(PrintText(next));
            next++;
        }
        
    }
    private bool CheckTextOverFlow()
    {
        float textboxwidth = t.preferredWidth;
        float textboxheight = t.preferredHeight;
        TextGenerator gen = t.cachedTextGenerator;

        return false;
    }
}

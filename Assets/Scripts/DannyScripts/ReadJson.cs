using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using LightJson;
using LightJson.Serialization;
using System.Reflection;
//Author Danny Diep
public class ReadJson : MonoBehaviour {

    /* Attach this script to a Text UI object
     * Specify the text to be loaded in the fileToRead field 
     * Specify how fast text is printed in textSpeed field
     * Fastest speed is 0.01f, slow is 0.3f adjust to taste
     * Files are currently read from Resources folder in Unity Editor
     * 
     */


    // Use this for initialization
    JsonObject jsonFile;
    RectTransform canvasRect;
    Text t;
    JsonArray values;
    int next = 0;
    [SerializeField]
    string fileToRead = "BaseJson.json";
    [SerializeField]
    string subsection = "dialogue";
    [SerializeField]
    float textSpeed = 0.01f;
    //float slowSpeed = 0.3f;
    //float fastSpeed = 0.01f;
    bool coroutineRunning = false;

    void Start () {
        jsonFile = new JsonObject();
        string datapath = Application.dataPath + "/Resources/" + fileToRead;
        jsonFile = JsonReader.ParseFile(datapath);

        string json = jsonFile.ToString();
        t = this.GetComponentInChildren<Text>();
        values = JsonValue.Parse(json)[subsection].AsJsonArray;
        ScrollText();
    }
    public void Reset()
    {
        t.text = "";
        next = 0;
    }
    public void SetParameters(string newFile = "BaseJson.json", string newSubsection = "dialogue")
    {
        string datapath = Application.dataPath + "/Resources/" + newFile;
        jsonFile = JsonReader.ParseFile(datapath);
        string json = jsonFile.ToString();
        values = JsonValue.Parse(json)[subsection].AsJsonArray;
    }
    private void OnEnable()
    {
        Reset();
        ScrollText();
    }
    private void Update()
    {
        //CheckTextOverFlow();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Update 1 Space");
            if(coroutineRunning == false)
            {
                Debug.Log("Update 2 Space");
                ScrollText();
            }
        }
    }
    IEnumerator PrintText(int index)
    {
        yield return null;
        string sentence = values[index];
        string output = "";
        foreach (char word in sentence )
        {
            output += word;
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Print now");
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
        Debug.Log("Finished Printing");
        coroutineRunning = false;
    }
    
    void ScrollText()
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
    bool CheckTextOverFlow()
    {
        float textboxwidth = t.preferredWidth;
        float textboxheight = t.preferredHeight;
        TextGenerator gen = t.cachedTextGenerator;
        Debug.Log("Pref Height: " + textboxheight);
        Debug.Log("Pref Width: " + textboxwidth);
        //float canvaswidth = canvasRect.GetComponentInParent<RectTransform>();
        return false;
    }
}

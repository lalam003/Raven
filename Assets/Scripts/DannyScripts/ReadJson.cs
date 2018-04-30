using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using LightJson;
using LightJson.Serialization;
using System.Reflection;

public class ReadJson : MonoBehaviour {


    // Use this for initialization
    JsonObject jsonFile;
    RectTransform canvasRect;
    Text t;
    JsonArray values;
    int next = 0;
    [SerializeField]
    string fileToRead = "BaseJson.json";
    [SerializeField]
    float textSpeed = 0.1f;
    //float slowSpeed = 0.3f;
    //float fastSpeed = 0.01f;
    bool coroutineRunning = false;

    void Start () {
        jsonFile = new JsonObject();
        string datapath = Application.dataPath + "/Resources/" + fileToRead;
        jsonFile = JsonReader.ParseFile(datapath);

        string json = jsonFile.ToString();
        t = this.GetComponent<Text>();
        values = JsonValue.Parse(json)["dialogue"].AsJsonArray;
    }
    private void Update()
    {
        //CheckTextOverFlow();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Update 1 Space");
            if(!coroutineRunning)
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
            Debug.Log("End of Dialogue");
            t.text = "";
            return;
        }
        coroutineRunning = true;
        StartCoroutine(PrintText(next));
        next++;
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

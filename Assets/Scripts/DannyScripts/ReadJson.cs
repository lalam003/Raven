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
    void Start () {
        jsonFile = new JsonObject();
        string datapath = Application.dataPath + "/Resources/BaseJson.json";
        jsonFile = JsonReader.ParseFile(datapath);

        string json = jsonFile.ToString();
        Debug.Log(json);
        t = this.GetComponent<Text>();
        values = JsonValue.Parse(json)["dialogue"].AsJsonArray;
        Debug.Log(values);
        //t.GetComponent<TextGenerationSettings>();
        CheckTextOverFlow();
    }
    private void Update()
    {
        //CheckTextOverFlow();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ScrollText();
        }
    }
    void ScrollText()
    {
        if(next >= values.Count)
        {
            Debug.Log("End of Dialogue");
            return;
        }
        t.text = values[next];
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using LightJson;
using System.IO;
public class MenuAddons {

    // Use this for initialization
    [MenuItem("Tools/Clear PlayerPrefs")]
    private static void NewMenuOption()
    {
        PlayerPrefs.DeleteAll();
    }
    [MenuItem("Assets/Create/Create Json")]
    private static void CreateJson()
    {
        // Create and add a new default Json for testing and other things.
        var json = new JsonObject()
        .Add("menu", new JsonArray()
        .Add("home")
        .Add("projects")
        .Add("about")
        ).ToString(true);
        string str = json.ToString();
        string path = Application.dataPath + "/Resources/BaseJson.json";
        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(str);
            }
        }
        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif

    }
}

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
        var json = new JsonObject()
        .Add("menu", new JsonArray()
        .Add("home")
        .Add("projects")
        .Add("about")
        ).ToString(true);

        // Create and add a new default Json for testing and other things.
        string datapath = Application.dataPath + "/Resources/BaseJson.json";
        using (FileStream fstream = new FileStream(datapath, FileMode.Create))
        {
            using (StreamWriter swriter = new StreamWriter(fstream))
            {
                swriter.Write(json);
            }
        }
        UnityEditor.AssetDatabase.Refresh();
    }
    [MenuItem("Assets/Create/Create Event")]
    public static Event Create()
    {
        Event asset = ScriptableObject.CreateInstance<Event>();
        AssetDatabase.CreateAsset(asset, "Assets/Event.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}

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
    public static JsonObject ParseJsonFile(string filePath)
    {
        JsonObject data = new JsonObject();
        try
        {
            data = JsonReader.ParseFile(filePath);
        }
        catch(JsonParseException exception)
        {
            Debug.LogError(exception);
        }

        return data;
    }
}

using LightJson;
using LightJson.Serialization;
using UnityEngine;
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

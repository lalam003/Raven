using LightJson;
using LightJson.Serialization;
using UnityEngine;

public class FileParser : MonoBehaviour {

    public JsonObject ParseJsonObject(string filePath)
    {
        JsonObject data = new JsonObject();
        try
        {
            data = JsonReader.ParseFile(filePath);
        }
        catch(JsonParseException exception)
        {
            Debug.Log("Failed to get json" + exception);
        }

        return data;
    }
}

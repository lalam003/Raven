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

        }

        return data;
    }
}

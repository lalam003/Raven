using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using LightJson;
using LightJson.Serialization;
public static class SaveLoad
{
    public static List<PlayerData> saveData = new List<PlayerData>();

    //Stores most recently loaded data publicly accessible by other classes
    public static PlayerData loadData;// = new PlayerData();
    
    public static void SaveGame(PlayerData player, string filename)
    {
        string json = JsonUtility.ToJson(player);
        saveData.Add(PlayerData.currentPlayer);
        string dataPath = Application.persistentDataPath + "/" + filename + ".json";
        using (FileStream fstream = new FileStream(dataPath, FileMode.Create))
        {
            using (StreamWriter swriter = new StreamWriter(fstream))
            {
                swriter.Write(json);
            }
        }
    }
    public static void LoadGame(string filename)
    {
        string dataPath = Application.persistentDataPath + "/" + filename + ".json";
        if (File.Exists(dataPath))
        {
            JsonObject data = ReadJson.ParseJsonFile(dataPath);
            loadData = JsonUtility.FromJson<PlayerData>(data.ToString());           
        }
        
    }

    //Deletes saves, untested
    public static void DeleteSave(string filename)
    {
        string dataPath = Application.persistentDataPath + "/" + filename + ".json";
        if (File.Exists(dataPath))
        {
            File.Delete(dataPath);
            
        }
    }
}

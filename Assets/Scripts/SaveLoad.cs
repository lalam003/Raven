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
    public static PlayerData loadData = new PlayerData();
    public static void SaveGame()
    {
        string json = JsonUtility.ToJson(PlayerData.currentPlayer);
        saveData.Add(PlayerData.currentPlayer);
        string dataPath = Application.persistentDataPath + "/savegame.json";
        using (FileStream fstream = new FileStream(dataPath, FileMode.Create))
        {
            using (StreamWriter swriter = new StreamWriter(fstream))
            {
                swriter.Write(json);
            }
        }
    }
    public static void LoadGame()
    {
        if(File.Exists(Application.persistentDataPath + "/savedgame.json"))
        {
            JsonObject data = ReadJson.ParseJsonFile(Application.persistentDataPath + "/savedgame.json");
            loadData = JsonUtility.FromJson<PlayerData>(data.ToString());
        }
    }
}

using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveData(PlayerData playerData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string savePath = Application.persistentDataPath + "/playerData.sav";

        FileStream stream = new FileStream(savePath, FileMode.Create);

        formatter.Serialize(stream, playerData);

        stream.Close();
    }

    public static PlayerData LoadData()
    {
        string savePath = Application.persistentDataPath + "/playerData.sav";

        if (File.Exists(savePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(savePath, FileMode.Open);

            PlayerData playerData = formatter.Deserialize(stream) as PlayerData;

            stream.Close();

            return playerData;
        }
        else
        {
            Debug.LogError("Save file not found at " + savePath);
            return null;
        }
    }

    public static void DeleteData()
    {
        string savePath = Application.persistentDataPath + "/playerData.sav";

        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }
        else
        {
            Debug.LogError("Save file not found at " + savePath);
        }
    }
}

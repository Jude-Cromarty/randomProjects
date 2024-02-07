using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class GameSaveManager : MonoBehaviour
{
    private const string SaveFileName = "game_save.json";

    [System.Serializable]
    private class SaveData
    {
        public List<GameObjectData> objectDataList;
    }

    [System.Serializable]
    private class GameObjectData
    {
        public string prefabName;
        public float positionX;
        public float positionY;
        public float positionZ;
        public float rotationX;
        public float rotationY;
        public float rotationZ;
        public string parentName;
    }

    private void Awake()
    {
        LoadGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            WipeData();
        }
    }

    private void SaveGame()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("SaveableObject");

        List<GameObjectData> objectDataList = new List<GameObjectData>();

        foreach (GameObject obj in gameObjects)
        {
            GameObjectData objectData = new GameObjectData();
            objectData.prefabName = RemoveCloneSuffix(obj.name);
            objectData.positionX = obj.transform.position.x;
            objectData.positionY = obj.transform.position.y;
            objectData.positionZ = obj.transform.position.z;
            objectData.rotationX = obj.transform.rotation.eulerAngles.x;
            objectData.rotationY = obj.transform.rotation.eulerAngles.y;
            objectData.rotationZ = obj.transform.rotation.eulerAngles.z;
            objectData.parentName = obj.transform.parent != null ? obj.transform.parent.name : string.Empty;

            objectDataList.Add(objectData);
            Debug.Log("Saved: " + objectData.prefabName);
        }

        SaveData saveData = new SaveData();
        saveData.objectDataList = objectDataList;

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(GetSaveFilePath(), json);

        Debug.Log("Game saved.");
    }

    private void LoadGame()
    {
        string filePath = GetSaveFilePath();

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            if (saveData != null && saveData.objectDataList != null)
            {
                Dictionary<string, GameObject> prefabDictionary = new Dictionary<string, GameObject>();

                foreach (GameObjectData objectData in saveData.objectDataList)
                {
                    string prefabName = RemoveCloneSuffix(objectData.prefabName);
                    float positionX = objectData.positionX;
                    float positionY = objectData.positionY;
                    float positionZ = objectData.positionZ;
                    float rotationX = objectData.rotationX;
                    float rotationY = objectData.rotationY;
                    float rotationZ = objectData.rotationZ;
                    string parentName = objectData.parentName;

                    Debug.Log("Loading: " + prefabName);

                    if (!prefabDictionary.ContainsKey(prefabName))
                    {
                        GameObject prefab = Resources.Load<GameObject>(prefabName);
                        if (prefab != null)
                        {
                            prefabDictionary[prefabName] = prefab;
                        }
                        else
                        {
                            Debug.LogWarning("Failed to load prefab: " + prefabName);
                            continue;
                        }
                    }

                    GameObject instantiatedPrefab = Instantiate(prefabDictionary[prefabName], new Vector3(positionX, positionY, positionZ), Quaternion.Euler(rotationX, rotationY, rotationZ));

                    if (!string.IsNullOrEmpty(parentName))
                    {
                        GameObject parentObject = GameObject.Find(parentName);
                        if (parentObject != null)
                        {
                            instantiatedPrefab.transform.parent = parentObject.transform;
                        }
                        else
                        {
                            Debug.LogWarning("Parent object not found: " + parentName);
                        }
                    }
                }

                Debug.Log("Game loaded.");
                return;
            }
        }

        Debug.Log("No saved data found.");
    }

    private void WipeData()
    {
        if (File.Exists(GetSaveFilePath()))
        {
            File.Delete(GetSaveFilePath());
            Debug.Log("Data wiped.");
        }
        else
        {
            Debug.Log("No data to wipe.");
        }
    }

    private string GetSaveFilePath()
    {
        return Application.persistentDataPath + "/" + SaveFileName;
    }

    private string RemoveCloneSuffix(string name)
    {
        const string cloneSuffix = "(Clone)";
        if (name.EndsWith(cloneSuffix))
        {
            return name.Substring(0, name.Length - cloneSuffix.Length);
        }
        return name;
    }
}

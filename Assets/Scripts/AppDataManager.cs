using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;    // For File class

[Serializable]
public class AppData
{
    public string storedData;
}


public class AppDataManager: MonoBehaviour
{
    private string dataPath;

    // Start is called before the first frame update
    void Start()
    {
        dataPath = Application.persistentDataPath + "/appData.json";
        if(!File.Exists(dataPath))
        {
            SaveData();
        }
        LoadData();
    }

    void OnApplicationQuit()    // OnAppplicationQuit() is not called for iOS because they normally suspend, not quit.
    {
        SaveData();
    }

    void OnDestroy()
    {
        SaveData();
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    private void LoadData()
    {
        if(File.Exists(dataPath))
        {
            string jsonData = File.ReadAllText(dataPath);
            AppData appData = JsonUtility.FromJson<AppData>(jsonData);
            CustomLogger.Print(this, $"appData: {appData}");
            CustomLogger.Print(this, $"appData.storedData : {appData.storedData}");
        }
        else
        {
            CustomLogger.Print(this, $"{dataPath} does not exit.");
        }
    }

    private void SaveData()
    {
        AppData appData = new AppData();
        int i = 0;
        appData.storedData = $"{++i} times saved.";
        string jsonData = JsonUtility.ToJson(appData);
        File.WriteAllText(dataPath, jsonData);
        CustomLogger.Print(this, "appData is saved.");
    }
}

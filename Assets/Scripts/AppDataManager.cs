using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;    // For File class


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


    void OnDestroy()
    {
        SaveData();
    }


    private void LoadData()
    {
        if(File.Exists(dataPath))
        {
            CustomLogger.Print(this, $"{dataPath} exists.");
            AppData.HorseName = File.ReadAllText(dataPath);
            string horseName = AppData.HorseName;
            CustomLogger.Print(this, $"(Previous) horseName : {horseName}");
        }
        else
        {
            CustomLogger.Print(this, $"{dataPath} does not exit.");
        }
    }

    private void SaveData()
    {
        File.WriteAllText(dataPath, AppData.HorseName);
        CustomLogger.Print(this, $"{AppData.HorseName} is saved.");
    }
}

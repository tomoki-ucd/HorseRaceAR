using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class RaceResultManager : MonoBehaviour
{
    // Static Field
    public static RaceResultManager Instance{get; private set;}
    
    // Instant Field
    private string[] _orderOfFinish = new string[Horse.NUM_OF_HORSES];
    public string[] orderOfFinish
    {
        get
        {
            return _orderOfFinish;
        }
        set
        {
            _orderOfFinish = value;
            if(_orderOfFinish[Horse.NUM_OF_HORSES-1] != null)
            {
                DisplayRaceResult();
            }
        }
    }

    // Instant Field
    [SerializeField] private GameObject _orderOfFinishTable;


    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 1;
    }


//    private static void DisplayRaceResult()
    private void DisplayRaceResult()
    {
        Transform content = _orderOfFinishTable.transform.Find("Scroll View/Viewport/Content");
        for(int i = 0; i < Horse.NUM_OF_HORSES; i++)
        {
            content.GetChild(i).Find("Horse").GetComponent<TextMeshProUGUI>().text = _orderOfFinish[i];
        }
        _orderOfFinishTable.SetActive(true);

        if(_orderOfFinish[0] == HorseSelector.selectedHorseName)
        {
            CustomLogger.Print(this, "You win the bet!");
        }
        else
        {
            CustomLogger.Print(this, "You lose the bet.");
        }
    }
}

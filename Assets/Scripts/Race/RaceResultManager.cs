using System;
using System.Collections;
using System.Collections.Generic;
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
            CustomLogger.Print(this, $"orderOfFinish{{set;}} is called. value is {value}");
            _orderOfFinish = value;
            CustomLogger.Print(this, $"_orderOfFinish : {_orderOfFinish}");
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
        _orderOfFinishTable.SetActive(true);
    }
}

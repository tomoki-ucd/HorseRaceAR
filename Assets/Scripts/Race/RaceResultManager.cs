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
    private const int NUM_OF_COLUMN = 2;    // Horse Number column, Horse Name column
    
    // Instant Field
    private string[,] _orderOfFinish = new string[Horse.NUM_OF_HORSES, NUM_OF_COLUMN];
    public string[,] orderOfFinish
    {
        get
        {
            return _orderOfFinish;
        }
        set
        {
            _orderOfFinish = value;
            if(_orderOfFinish[Horse.NUM_OF_HORSES-1,0] != null)
            {
                DisplayRaceResult();
            }
        }
    }

    [SerializeField] private GameObject _orderOfFinishTable;
    [SerializeField] private GameObject _youWin;
    [SerializeField] private GameObject _youLose;
    [SerializeField] private GameObject _playAgainButton;

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


//    private static void DisplayRaceResult()
    private void DisplayRaceResult()
    {
        Transform content = _orderOfFinishTable.transform.Find("Scroll View/Viewport/Content");
        for(int i = 0; i < Horse.NUM_OF_HORSES; i++)
        {
            content.GetChild(i).Find("Horse").GetComponent<TextMeshProUGUI>().text = orderOfFinish[i, 0] + " " + orderOfFinish[i, 1];
        }
        _orderOfFinishTable.SetActive(true);

        if(_orderOfFinish[0, 1] == HorseSelector.selectedHorseName)
        {
            CustomLogger.Print(this, "You win the bet!");
            _youWin.SetActive(true);
        }
        else
        {
            CustomLogger.Print(this, "You lose the bet.");
            _youLose.SetActive(true);
        }

        _playAgainButton.SetActive(true);
    }
}

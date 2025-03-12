using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Make horses run.
/// </summary>
// TO DO : Change the naming to RaceController 
public class RaceController : MonoBehaviour
{
    public ARPlaneController _arPlaneController;
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Button _startStopButton;
    [SerializeField] Horse[] horses;
    private bool _isRaceStarted = false;
    bool _isRunning = false;


    // Start is called before the first frame update
    void Start()
    {
        if(_arPlaneController == null)
        {
            CustomLogger.Print(this, $"_arPlaneController is null.");
        }

        if(_startStopButton == null)
        {
            CustomLogger.Print(this, $"_startButto is null.");
        }
        else
        {
            _startStopButton.onClick.AddListener(StartStopRace);
        }

        // Subscribe to Start Button
        if(_toggle == null)
        {
            CustomLogger.Print(this, $"_toggle is null.");
        }
        else{
            _toggle.onValueChanged.AddListener(ToggleStartButtonVisibility);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(_isRaceStarted)    
        {
            if(_isRunning)
            {
                RunHorse();
            }
        }
    }


    /// <summary>
    /// Show StartButton when <paramref name="isOn"/> is false.
    /// <param name="isOn"> Toggle state </param> 
    /// </summary>
    private void ToggleStartButtonVisibility(bool isOn)
    {
        _startStopButton.gameObject.SetActive(isOn);
    }


    /// <summary>
    /// Start the race.
    /// </summary>
    private void StartStopRace()
    {
        // Initialize horses and set `_isRaceStarted` true.
        // This is done only once.
        if(!_isRaceStarted)
        {
            horses = FindObjectsOfType<Horse>();
            if(horses == null)
            {
                CustomLogger.Print(this, "_horse is null.");
            }
            // Debug log
            int horseNum = 1;
            foreach(Horse horse in horses)
            {
                CustomLogger.Print(this, $"horse {horseNum} : {horse}");
            }
            _isRaceStarted = true;
            _isRunning = true;

            return;
        }

        // Stop horse when StartStop button is tapped while horses are running.
        if(_isRunning)
        {
            StopHorse();
        }
        else
        {
            RunHorse();
        }
    }


    /// <summary>
    /// Ran Horses.
    /// </summary>
    private void RunHorse()
    {
        _isRunning = true;

        for(int i = 0; i < horses.Length; i++)
        {
            Horse horse = horses[i];
            horse.Run();
        }
    }


    /// <summary>
    /// Stop running horses.
    /// </summary>
    void StopHorse()
    {
        _isRunning = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Make horses run.
/// </summary>
// TO DO : Change the naming to RaceController 
public class RunningHorseController : MonoBehaviour
{
    public HorseSpawner _horseSpawner;
    public ARPlaneController _arPlaneController;
    private GameObject _racetrack;
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Button _startButton;
    [SerializeField] Horse[] horses;
    private bool _isRaceStarted = false;


    // Start is called before the first frame update
    void Start()
    {
        // Set horse and racetrack objects.
//        if(_horseSpawner == null) 
//        {
//            CustomLogger.Print(this, $"_horseSpawner is null.");
//        } 
        // Check Horse assignment.



        if(_arPlaneController == null)
        {
            CustomLogger.Print(this, $"_arPlaneController is null.");
        }

        if(_startButton == null)
        {
            CustomLogger.Print(this, $"_startButto is null.");
        }
        else
        {
            _startButton.onClick.AddListener(StartRace);
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
            RunHorse();
        }
    }

    /// <summary>
    /// Show StartButton when toggle is false.
    /// </summary>
    private void ToggleStartButtonVisibility(bool isOn)
    {
        _startButton.gameObject.SetActive(isOn);
    }

    /// <summary>
    /// Start the race.
    /// </summary>
    private void StartRace()
    {
        if(_isRaceStarted)
        {
            return;
        }

        // Assign available Horse(s) to `horses` array
        _racetrack = _arPlaneController.SpawnedRacetrack;
//        _horse = _horseSpawner.SpawnedHorse;
        horses = FindObjectsOfType<Horse>();

        if(_racetrack == null)
        {
            CustomLogger.Print(this, "_racetrack is null.");
        }
        else if(horses == null)
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
    }


    /// <summary>
    /// Ran Horses.
    /// </summary>
    private void RunHorse()
    {
//        Vector3 incremental= new Vector3(0.01f, 0, 0);
//        Vector3 newLocalPosition = _horse.transform.localPosition + incremental;
//        Vector3 newWorldPosition = _racetrack.transform.TransformPoint(newLocalPosition);
//        _horse.transform.position = newWorldPosition;
        for(int i = 0; i < horses.Length; i++)
        {
            Horse horse = horses[i];
            horse.Run();
        }
    }
}

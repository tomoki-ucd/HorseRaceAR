using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Make horses run.
/// </summary>
public class RunningHorseController : MonoBehaviour
{
    public HorseSpawner _horseSpawner;
    public ARPlaneController _arPlaneController;
    private GameObject _horse;
    private GameObject _racetrack;
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Button _startButton;
    private bool _isRaceStarted = false;


    // Start is called before the first frame update
    void Start()
    {
        // Set horse and racetrack objects.
        if(_horseSpawner == null) 
        {
            CustomLogger.Print(this, $"_horseSpawner is null.");
        } 

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
        _racetrack = _arPlaneController.SpawnedRacetrack;
        _horse = _horseSpawner.SpawnedHorse;

        if(_racetrack == null)
        {
            CustomLogger.Print(this, "_racetrack is null.");
        }
        else if(_horse == null)
        {
            CustomLogger.Print(this, "_horse is null.");
        }
        _isRaceStarted = true;
    }


    /// <summary>
    /// Ran Horses.
    /// </summary>
    private void RunHorse()
    {
        Vector3 currentPosition = _racetrack.transform.GetChild(0).transform.localPosition;
        Vector3 newPosition = currentPosition;
        newPosition.x += 0.01f;
        _horse.transform.position = newPosition;
    }
}

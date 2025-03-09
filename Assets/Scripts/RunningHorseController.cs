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
        else{
            _racetrack = _arPlaneController.SpawnedRacetrack;
        }

        if(_startButton == null)
        {
            CustomLogger.Print(this, $"_startButto is null.");
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
        
    }

    /// <summary>
    /// Show StartButton when toggle is false.
    /// </summary>
    private void ToggleStartButtonVisibility(bool isOn)
    {
        _startButton.gameObject.SetActive(isOn);
    }
}

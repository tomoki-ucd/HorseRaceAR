using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Provides the functionality to run and stop horses.
/// </summary>
public class RaceController : MonoBehaviour
{
    // Instant Fields
    [SerializeField] private GameObject _startStopButton;
    [SerializeField] private RacetrackSpawner _racetrackSpawner;
    private Horse[] horses;
    /// <summary>
    /// _isRaceStarted is used to prevent repetitive initialization of horses field.
    /// </summary>
    private bool _isRaceStarted = false;
    bool _isRunning = false;


    // Start is called before the first frame update
    void Start()
    {
        Initialize();
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

    private void Initialize()
    {
        // Subscribe to Start/Stop Button event
        if(_startStopButton == null)
        {
            CustomLogger.Print(this, $"_startButton is null.");
        }
        else
        {
            _startStopButton.GetComponent<Button>().onClick.AddListener(StartStopRace);
        }

        // Subscribe to OnRacetrackSpanwned event
        if(_racetrackSpawner == null)
        {
            CustomLogger.Print(this, $"_racetrackSpawner is null.");
        }
        else
        {
            _racetrackSpawner.OnRacetrackSpawned += HandleSpawnedRacetrack;
        }
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
            StartHorse();
            _isRaceStarted = true;

            return;
        }

        // Stop horse when StartStop button is tapped while horses are running.
        if(_isRunning)
        {
            StopHorse();
        }
        else
        {
            StartHorse();
        }
    }


    /// <summary>
    /// Ran Horses.
    /// </summary>
    private void RunHorse()
    {
        for(int i = 0; i < horses.Length; i++)
        {
            Horse horse = horses[i];
            horse.Run();
        }
    }

    /// <summary>
    /// Set _isRunning flag to true, meaning horses run.
    /// </summary>
    private void StartHorse()
    {
        _isRunning = true;
        _startStopButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "STOP";
    }

    /// <summary>
    /// Set _isRunning flag to false, meaning stop horses.
    /// </summary>
    private void StopHorse()
    {
        _isRunning = false;
        _startStopButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "START";
    }


    private void HandleSpawnedRacetrack(GameObject spawnedRacetrack)
    {
        CustomLogger.Print(this, "HandleSpawnedRacetrack() is called.");    // Debug
        _startStopButton.SetActive(true);
    }
}

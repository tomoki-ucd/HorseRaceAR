using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Provides the functionality to run and stop horses.
/// </summary>
public class RaceController : MonoBehaviour
{
    public RacetrackSpawner _racetrackSpawner;
//    [SerializeField] private Toggle _setHorseButton;
    [SerializeField] private Button _setHorseButton;
    [SerializeField] private Button _startStopButton;
    [SerializeField] Horse[] horses;
    private bool _isRaceStarted = false;
    bool _isRunning = false;


    // Start is called before the first frame update
    void Start()
    {
        if(_racetrackSpawner == null)
        {
            CustomLogger.Print(this, $"_racetrackSpawner is null.");
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
        if(_setHorseButton == null)
        {
            CustomLogger.Print(this, $"_setHorseButton is null.");
        }
        else{
            _setHorseButton.onClick.AddListener(DisplayStartButton);
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
    private void DisplayStartButton()
    {
        _startStopButton.gameObject.SetActive(true);
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

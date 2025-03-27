using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Provides the functionality to run and stop horses.
/// </summary>
public class RaceController : MonoBehaviour
{
    // Instant Fields
    [SerializeField] private Button _startStopButton;
    private Horse[] horses;
    /// <summary>
    /// _isRaceStarted is used to prevent repetitive initialization of horses field.
    /// </summary>
    private bool _isRaceStarted = false;
    bool _isRunning = false;


    // Start is called before the first frame update
    void Start()
    {
        if(_startStopButton == null)
        {
            CustomLogger.Print(this, $"_startButto is null.");
        }
        else
        {
            _startStopButton.onClick.AddListener(StartStopRace);
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
    void StartHorse()
    {
        _isRunning = true;
        _startStopButton.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "STOP";
    }

    /// <summary>
    /// Set _isRunning flag to false, meaning stop horses.
    /// </summary>
    void StopHorse()
    {
        _isRunning = false;
        _startStopButton.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "START";
    }
}

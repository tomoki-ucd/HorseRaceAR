using System.Collections;
using System.Collections.Concurrent;
using UnityEngine;
using UnityEngine.XR.ARFoundation.VisualScripting;

/// <summary>
/// Represents a horse.
/// </summary>
public class Horse : MonoBehaviour
{
    // Static fields
    // TO DO : Consider to add `Horse[] horses` property
    const float MIN_SPEED = 0.5f;
    const float MAX_SPEED = 1.5f;
    public const int NUM_OF_HORSES = 3;
    const int NUM_OF_SPEED_STAGE = 3;   // A horse's speed changes at each stage.
                                        // The value = 3 means it runs at 3 different speed in a race.
    readonly static Vector3 _runDistancePerFrame = new Vector3(-0.01f, 0, 0);

    // Instance fields
    // TO DO : Repace _racetrackSpawner with Racetrack object after Racetrack class is added.
    [SerializeField] private RacetrackSpawner _racetrackSpawner;
    [SerializeField] private RaceResultManager _raceResultManager;
    float[] _speeds = new float[NUM_OF_SPEED_STAGE];
    float _currentSpeed = 0;
    float _runDistance = 0; // The.COURSE_DISTANCE that the horse has run.
    private bool _isFinished{get; set;} = false;
    public int horseNumber{get; set;}


    void Awake()
    {
        for(int i = 0; i < _speeds.Length; i++)
        {
            _speeds[i] = SetSpeedRandomly();
//            CustomLogger.Print(this, $"_horseSpeed.speeds[{i}] : {_speeds[i]}");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _racetrackSpawner = FindObjectOfType<RacetrackSpawner>();
        _raceResultManager = FindObjectOfType<RaceResultManager>();

        StartCoroutine(LogEverySecondCoroutine());  // Debug
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Run.
    /// </summary>
    public void Run()
    {
        if(_isFinished)
        {
            return;
        }

        Vector3 newLocalPosition = transform.localPosition;
        Vector3 newWorldPosition;
        Vector3 advancedAmount;

        if(_runDistance < Racetrack.COURSE_DISTANCE * (1.0f/3.0f))   // 1st one-third of the race.
        {
            _currentSpeed = _speeds[0];
        }
        else if(_runDistance < Racetrack.COURSE_DISTANCE * (2.0f/3.0f))  // 2nd one-thrid of the race.
        {
            _currentSpeed = _speeds[1];
        }
        else if(_runDistance < Racetrack.COURSE_DISTANCE * (3.0f/3.0f))  // 3rd one-third of the race.
        {
            _currentSpeed = _speeds[2];
        }
        else
        {
//            _currentSpeed = 0;
            FinishGoal();
        }

        advancedAmount = _runDistancePerFrame * _currentSpeed;
        newLocalPosition += advancedAmount;

        // TO DO : If it finish the goal, it slows down.
        newWorldPosition = _racetrackSpawner.SpawnedRacetrack.transform.TransformPoint(newLocalPosition);
        transform.position = newWorldPosition;
        _runDistance += System.Math.Abs(advancedAmount.x);
    }

    /// <summary>
    /// Set speed.
    /// </summary>
    /// <remarks>
    /// Randomly set speed within the MIN-MAX range at each stage (early,mid,final).
    /// </remarks>
    float SetSpeedRandomly()
    {
        float speed = UnityEngine.Random.Range(MIN_SPEED, MAX_SPEED);
        return speed;
    }

    // For Debug
    IEnumerator LogEverySecondCoroutine()
    {
        while (true)
        {
            CustomLogger.Print(this, $"{name}, _runDistance : {_runDistance}, speed at {_currentSpeed}");
            yield return new WaitForSeconds(1);
        }
    }


    /// <summary>
    /// Finish Goal and stop running by setting `_isFinished` flag to true.
    /// Also register the finished horse to the order of finish.
    /// </summary>
    private void FinishGoal()
    {
        string[,] tmp = (string[,])_raceResultManager.orderOfFinish.Clone();
        for(int i = 0; i < tmp.Length; i++)
        {
            if(tmp[i, 0] == null)
            {
                tmp[i, 0] = "#" + horseNumber.ToString();
                tmp[i, 1] = gameObject.name;
                _raceResultManager.orderOfFinish = tmp;
                CustomLogger.Print(this, $"order[{i}] = {gameObject.name}");
                break;
            }
        }
        _isFinished = true;
    }
}

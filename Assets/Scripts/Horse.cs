using System.Collections;
using UnityEngine;

/// <summary>
/// Represents a horse.
/// </summary>
public class Horse : MonoBehaviour
{
    // Static fields
    // TO DO : Consider to add `Horse[] horses` property
    public const int NUM_OF_HORSES = 3;
    const int NUM_OF_SPEED_STAGE = 3;
    const float MIN_SPEED = 0.5f;
    const float MAX_SPEED = 1.5f;
    readonly static Vector3 _baseAmount = new Vector3(-0.01f, 0, 0);

    // Instance fields
    // TO DO : Repace _arPlaneController with Racetrack object after Racetrack class is added.
    ARPlaneController _arPlaneController;
    float[] _speeds = new float[NUM_OF_SPEED_STAGE];
    float _currentSpeed = 0;
    float _runDistance = 0; // The distance that the horse has run.


    void Awake()
    {
        for(int i = 0; i < _speeds.Length; i++)
        {
            _speeds[i] = SetSpeedRandomly();
            CustomLogger.Print(this, $"_horseSpeed.speeds[{i}] : {_speeds[i]}");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _arPlaneController = FindObjectOfType<ARPlaneController>();
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
        Vector3 newLocalPosition = transform.localPosition;
        Vector3 newWorldPosition;
        Vector3 advancedAmount;

        if(_runDistance < Racetrack.distance * (1.0f/3.0f))   // 1st one-third of the race.
        {
//            CustomLogger.Print(this, $"_runDistance < Racetrack.distance * (1/3) : {Racetrack.distance * (1.0f/3.0f)}");
            _currentSpeed = _speeds[0];
        }
        else if(_runDistance < Racetrack.distance * (2.0f/3.0f))  // 2nd one-thrid of the race.
        {
//            CustomLogger.Print(this, $"_runDistance < Racetrack.distance * (2/3) : {Racetrack.distance * (2.0f/3.0f)}");
            _currentSpeed = _speeds[1];
        }
        else if(_runDistance < Racetrack.distance * (3.0f/3.0f))  // 3rd one-third of the race.
        {
//            CustomLogger.Print(this, $"_runDistance < Racetrack.distance * (3/3) : {Racetrack.distance * (3/3)}");
            _currentSpeed = _speeds[2];
        }
        else
        {
            _currentSpeed = 0;
        }

        advancedAmount = _baseAmount * _currentSpeed;
        newLocalPosition += advancedAmount;

        // TO DO : If it finish the goal, it slows down.
        newWorldPosition = _arPlaneController.SpawnedRacetrack.transform.TransformPoint(newLocalPosition);
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
            CustomLogger.Print(this, $"{this.name}, _runDistance : {_runDistance}, speed at {_currentSpeed}");
            yield return new WaitForSeconds(1);
        }
    }
}

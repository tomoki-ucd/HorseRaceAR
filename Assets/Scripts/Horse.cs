using System.Collections;
using System.Collections.Generic;
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
    const float MIN_SPEED = 0.8f;
    const float MAX_SPEED = 1.2f;
    readonly static Vector3 _advanceAmount = new Vector3(-0.01f, 0, 0);

    // Instance fields
    float _distance = 0; // The distance that the horse has run.
    // TO DO : Repace _arPlaneController with Racetrack object after Racetrack class is added.
    ARPlaneController _arPlaneController;
    float[] _speeds = new float[NUM_OF_SPEED_STAGE];


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
        // TO DO : If it finish the goal, it slows down.
        Vector3 newLocalPosition = transform.localPosition + _advanceAmount;
        Vector3 newWorldPosition = _arPlaneController.SpawnedRacetrack.transform.TransformPoint(newLocalPosition);
        transform.position = newWorldPosition;
        _distance += _advanceAmount.x;
    }

    /// <summary>
    /// Set speed.
    /// </summary>
    /// <remarks>
    /// Randomly set speed within the MIN-MAX range at each stage (early,mid,final).
    /// </remarks>
    float SetSpeedRandomly()
    {
        float speed = Random.Range(MIN_SPEED, MAX_SPEED);
        return speed;
    }
}

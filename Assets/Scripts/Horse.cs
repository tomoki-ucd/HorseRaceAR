using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// Represents a horse.
/// </summary>
public class Horse : MonoBehaviour
{
    // Static fields
    // TO DO : Consider to add `Horse[] horses` property
    readonly static Vector3 _advanceAmount = new Vector3(-0.01f, 0, 0);
    public const int NUM_OF_HORSES = 3;

    // Instance fields
    float _distance = 0; // The distance that the horse has run.
    public HorseSpeed speed = new HorseSpeed(1f, 1f, 1f);   // The speed at each stage of the race.
    // TO DO : Repace _arPlaneController with Racetrack object after Racetrack class is added.
    ARPlaneController _arPlaneController;

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
}

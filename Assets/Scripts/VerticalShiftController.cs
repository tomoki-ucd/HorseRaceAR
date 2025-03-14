using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Provides the functionality of shifting objects vertically.
/// </summary>
public class VerticalShiftController: MonoBehaviour
{
    [SerializeField] private Button _upButton;
    [SerializeField] private Button _downButton;
    private RacetrackSpawner _racetrackSpawner;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize objects
        _racetrackSpawner = FindObjectOfType<RacetrackSpawner>();

        // Register methods to events
        _upButton.onClick.AddListener(RaiseRacetrack);
        _downButton.onClick.AddListener(LowerRacetrack);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// Raise the position of the objects.
    /// </summary>
    /// <param name=></param>
    /// <returns></return>
    private void RaiseRacetrack()
    {
        GameObject racetrack = _racetrackSpawner.SpawnedRacetrack;

        if(racetrack == null)
        {
            CustomLogger.Print(this, "racetrack is null.");
            return;
        }
        Vector3 racetrackPos = racetrack.transform.position;
        racetrackPos.y += 0.1f;
        racetrack.transform.position = racetrackPos;
    }


    /// <summary>
    /// Lower the position of Racetrack object.
    /// </summary>
    /// <param name=""></param>
    /// <returns></return>
    private void LowerRacetrack()
    {
        GameObject racetrack = _racetrackSpawner.SpawnedRacetrack;

        if(racetrack == null)
        {
            CustomLogger.Print(this, "racetrack is null.");
            return;
        }
        Vector3 racetrackPos = racetrack.transform.position;
        racetrackPos.y -= 0.1f;
        racetrack.transform.position = racetrackPos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Provides the functionality of shifting objects vertically.
/// </summary>
public class VerticalShiftController: MonoBehaviour
{
    [SerializeField] private RacetrackSpawner _racetrackSpawner;
    [SerializeField] private Button _upButton;
    [SerializeField] private Button _downButton;
    private GameObject _racetrack;

    // Start is called before the first frame update
    void Start()
    {
        // Register methods to events
        _upButton.onClick.AddListener(RaiseRacetrack);
        _downButton.onClick.AddListener(LowerRacetrack);
        _racetrackSpawner.OnRacetrackSpawned += HandleSpawnedRacetrack;
    }


    void OnDestroy()
    {
        _upButton.onClick.RemoveListener(RaiseRacetrack);
        _downButton.onClick.RemoveListener(LowerRacetrack);
        _racetrackSpawner.OnRacetrackSpawned -= HandleSpawnedRacetrack;
    }

    /// <summary>
    /// Set the spawned racetrack to _racetrack member.
    /// </summary>
    private void HandleSpawnedRacetrack(GameObject spawnedRacetrack)
    {
        CustomLogger.Print(this, "HandleSpawnedRacetrack is called");
        _racetrack = spawnedRacetrack;
    }

    /// <summary>
    /// Raise the position of the objects.
    /// </summary>
    /// <param name=></param>
    /// <returns></return>
    private void RaiseRacetrack()
    {
        if(_racetrack == null)
        {
            CustomLogger.Print(this, "_racetrack is null.");
            return;
        }
        Vector3 _racetrackPos = _racetrack.transform.position;
        _racetrackPos.y += 0.1f;
        _racetrack.transform.position = _racetrackPos;
    }


    /// <summary>
    /// Lower the position of Racetrack object.
    /// </summary>
    /// <param name=""></param>
    /// <returns></return>
    private void LowerRacetrack()
    {
        if(_racetrack == null)
        {
            CustomLogger.Print(this, "_racetrack is null.");
            return;
        }
        Vector3 _racetrackPos = _racetrack.transform.position;
        _racetrackPos.y -= 0.1f;
        _racetrack.transform.position = _racetrackPos;
    }
}

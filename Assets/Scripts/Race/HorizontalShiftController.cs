using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalShiftController: MonoBehaviour
{
    // Instant Fields
    [SerializeField] private Joystick variableJoystick;
    [SerializeField] private RacetrackSpawner _racetrackSpawner;
    private GameObject _racetrack;


    // Start is called before the first frame update
    void Start()
    {
        _racetrackSpawner.OnRacetrackSpawned += HandleSpawnedRacetrack;
    }


    void Oestroy()
    {
        _racetrackSpawner.OnRacetrackSpawned -= HandleSpawnedRacetrack;
    }


    // Update is called once per frame
    void Update()
    {
        if(_racetrack == null)
        {
            return;
        }
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        _racetrack.transform.Translate(direction * Time.deltaTime, Space.World);
    }


    /// <summary>
    /// Set the spawned racetrack to _racetrack member.
    /// </summary>
    private void HandleSpawnedRacetrack(GameObject spawnedRacetrack)
    {
        CustomLogger.Print(this, "HandleSpawnedRacetrack is called");
        _racetrack = spawnedRacetrack;
    }
}

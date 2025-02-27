using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectShiftEventHandler: MonoBehaviour
{
    [SerializeField] private Button _upButton;
    [SerializeField] private Button _downButton;
    private ARPlaneController _arPlaneController;
    private ObjectPlacementEventHandler _objectPlacementEventHandler;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize objects
        _arPlaneController = FindObjectOfType<ARPlaneController>();
        _objectPlacementEventHandler = FindObjectOfType<ObjectPlacementEventHandler>();

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
        GameObject racetrack = _arPlaneController.SpawnedRacetrack;
        GameObject horse = _objectPlacementEventHandler.SpawnedHorse;

        if(racetrack == null)
        {
            CustomLogger.Print(this, "racetrack is null.");
            return;
        }
        Vector3 racetrackPos = racetrack.transform.position;
        racetrackPos.y += 0.1f;
        racetrack.transform.position = racetrackPos;

        if(horse == null)
        {
            CustomLogger.Print(this, "horse is null.");
        }
        else
        {
            Vector3 horsePos = horse.transform.position;
            horsePos.y += 0.1f;
            // TO DO: Consider to attach horse as a child GameObject to racetrack
            horse.transform.position = horsePos;
        }
    }


    /// <summary>
    /// Lower the position of Racetrack object.
    /// </summary>
    /// <param name=""></param>
    /// <returns></return>
    private void LowerRacetrack()
    {
        GameObject racetrack = _arPlaneController.SpawnedRacetrack;
        GameObject horse = _objectPlacementEventHandler.SpawnedHorse;

        if(racetrack == null)
        {
            CustomLogger.Print(this, "racetrack is null.");
            return;
        }
        Vector3 racetrackPos = racetrack.transform.position;
        racetrackPos.y -= 0.1f;
        racetrack.transform.position = racetrackPos;

        if(horse == null)
        {
            CustomLogger.Print(this, "horse is null.");
        }
        else
        {
            Vector3 horsePos = horse.transform.position;
            horsePos.y -= 0.1f;
            // TO DO: Consider to attach horse as a child GameObject to racetrack
            horse.transform.position = horsePos;
        }
    }
}

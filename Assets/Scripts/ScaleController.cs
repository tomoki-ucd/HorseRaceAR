using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;    // For EnhancedTouchSupport
using System;   // For Math


/// <summary>
/// Provides the functionality of scaling objects.
/// </summary>
public class ScaleController: MonoBehaviour
{
    // Constants
    private const float SCALE_MIN = 0.5f;
    private const float SCALE_MAX = 2.0f;

    // instant members
    private RacetrackSpawner _racetrackSpawner = null;
    private float? _initialPinchDistance;


    void Awake()
    {
        // Enable EnhancedTouch
        EnhancedTouch.EnhancedTouchSupport.Enable();
    }


    // Start is called before the first frame update
    void Start()
    {
        _racetrackSpawner = FindObjectOfType<RacetrackSpawner>();
    }


    // Update is called once per frame
    void Update()
    {
        // Has the racetrack already been spawned?
        GameObject spawnedRacetrack = _racetrackSpawner.SpawnedRacetrack;
        if(spawnedRacetrack == null)
        {
            return;
        }

        // Get a pinch based on two fingers
        if(EnhancedTouch.Touch.activeFingers.Count != 2)
        {
            _initialPinchDistance = null;
            return;
        }
        float pinchDistance = ComputePinchDistance(); 

        if(!_initialPinchDistance.HasValue) // Store pinch distance as the initial value if it's null.
        {
            _initialPinchDistance = pinchDistance;
        }
        else
        {
//            float newScaleRatio = pinchDistance / (float)_initialPinchDistance;
            // Moderate the change caused by pinchDistance by halving `diff` because the original above logic was too responsive.
            float diff = pinchDistance - (float)_initialPinchDistance;
            float newScaleRatio = ((float)_initialPinchDistance + diff / 4)/ (float)_initialPinchDistance;
            ScaleObject(spawnedRacetrack, newScaleRatio);
//            _initialPinchDistance = null; // Clearing _initialPinchDistance value here created unexpected scale change.
        }
    }


    /// <summary>
    /// Compute the pinch size (the distance between two fingers).
    /// </summary>
    private float ComputePinchDistance()
    {
        var finger1 = EnhancedTouch.Touch.activeFingers[0];
        var finger2 = EnhancedTouch.Touch.activeFingers[1];

        // Compute the distance between the fingers.
        float xDist = Math.Abs(finger1.screenPosition.x - finger2.screenPosition.x);
        float yDist = Math.Abs(finger1.screenPosition.y - finger2.screenPosition.y);
        float pinchDistance = (float)Math.Sqrt((xDist * xDist) + (yDist * yDist));
        return pinchDistance;
    }


    ///<summary>
    /// Scale the object based on the newScaleRatio.
    ///<param name="obj"> Object to scale </param>
    ///<param name="newScaleRatio"> Object is scaled to this ratio </param>
    // TO DO : Add `targetAxis` parameter that tells which axis, x, y, or z, are scaled.
    private void ScaleObject(GameObject racetrack, float newScaleRatio)
    {
        Vector3 currentScale = racetrack.transform.localScale;
        {
            var newScale = new Vector3(currentScale.x * newScaleRatio, 1f, currentScale.z * newScaleRatio);
            if(newScale.x < SCALE_MIN || newScale.x > SCALE_MAX)
            {
                return;
            }
            racetrack.transform.localScale = newScale;

            // Scale horses also in Y direction.
            GameObject horse = racetrack.transform.GetChild(0).gameObject;
            Vector3 horseScale = horse.transform.localScale;
            horse.transform.localScale = new Vector3(horseScale.x, horseScale.y * newScaleRatio, horseScale.z);
        }
    }
}

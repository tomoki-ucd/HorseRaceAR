using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;    // For EnhancedTouchSupport
using System;   // For Math

/// <summary>
/// Scale the object as per the finger pinch input
/// </summary>
public class ObjectScaler: MonoBehaviour
{
    // Brainstrom
    // Premise : 
    // - The racetrack is already placed
    // - 
    // What components are needed?
    // - Two finger touches
    // - The distance btw the fingers
    // - The original distance
        // - The original distance is valid until the same two fingers stay on the screen
    // - The new distance 
    // - The min and max scale (Tentative: The min : 0.5, The max : 2.0)

    // Constants
    private const float SCALE_MIN = 0.5f;
    private const float SCALE_MAX = 2.0f;

    // instant members
    private ARPlaneController _arPlaneController = null;
    private float _pinch_dist_orig = 0;


    void Awake()
    {
        // Enable EnhancedTouch
        EnhancedTouch.EnhancedTouchSupport.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        _arPlaneController = FindObjectOfType<ARPlaneController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Has the racetrack already been spawned?
        GameObject spawnedRacetrack = _arPlaneController.SpawnedRacetrack;
        if(spawnedRacetrack == null)
        {
            return;
        }

        // Get a pinch (two fingers)
        if(EnhancedTouch.Touch.activeFingers.Count != 2)
        {
            _pinch_dist_orig = 0;
            return;
        }
        var finger1 = EnhancedTouch.Touch.activeFingers[0];
        var finger2 = EnhancedTouch.Touch.activeFingers[1];

        // Compute the pinch change.
        // Compute the original distance btw the fingers.
        float x_dist = Math.Abs(finger1.screenPosition.x - finger2.screenPosition.x);
        float y_dist = Math.Abs(finger1.screenPosition.y - finger2.screenPosition.y);
        float pinch_dist = (float)Math.Sqrt((x_dist * x_dist) + (y_dist * y_dist));

        // Store the original distance.
        if(_pinch_dist_orig == 0)
        {
            _pinch_dist_orig = pinch_dist;
        }

        // Compute the new distance btw the fingers.
        // Already computed. Not necessary.

        // Compute the increased ratio from the original to the new distance.
        float newScaleRatio = pinch_dist / _pinch_dist_orig;
        CustomLogger.Print(this, $"newScaleRatio: {newScaleRatio}");

        // Scale object per the increased ratio from the original to the new distance.
//        Vector3 currentScale = spawnedRacetrack.transform.localScale;
//        if(currentScale.x < SCALE_MIN || currentScale.x > SCALE_MAX)
//        {
//            return;
//        }
//        var newScale = new Vector3(currentScale.x * newScaleRatio, 1f, currentScale.z * newScaleRatio);
//        if(newScale.x < SCALE_MIN || newScale.x > SCALE_MAX)
//        {
//            return;
//        }
//        _arPlaneController.SpawnedRacetrack.transform.localScale = newScale;
        spawnedRacetrack = ScaleObject(spawnedRacetrack, newScaleRatio);
    }

    ///<summary>
    /// Scale the object based on the newScaleRatio.
    ///<param name=obj> Object to scale </param>
    ///<param name=newScaleRatio> Object is scaled to this ratio </param>
    ///<return> GameObject after it is scaled </return>
    // TO DO : Add `targetAxis` parameter that tells which axis, x, y, or z, are scaled.
    private GameObject ScaleObject(GameObject obj, float newScaleRatio)
    {
        Vector3 currentScale = obj.transform.localScale;
//        if(currentScale.x < SCALE_MIN || currentScale.x > SCALE_MAX)
//        {
//            return;
//        }
//        var newScale = new Vector3(currentScale.x * newScaleRatio, 1f, currentScale.z * newScaleRatio);
//        if(newScale.x < SCALE_MIN || newScale.x > SCALE_MAX)
//        {
//            return;
//        }
//       obj.transform.localScale = newScale;
        if(currentScale.x > SCALE_MIN && currentScale.x < SCALE_MAX)
        {
            var newScale = new Vector3(currentScale.x * newScaleRatio, 1f, currentScale.z * newScaleRatio);
            if(newScale.x > SCALE_MIN && newScale.x < SCALE_MAX)
            {
                obj.transform.localScale = newScale;

                // Scale horses also in Y direction.
                GameObject horse = obj.transform.GetChild(0).gameObject;
                Vector3 horseScale = horse.transform.localScale;
                horse.transform.localScale = new Vector3(horseScale.x, horseScale.y * newScaleRatio, horseScale.z);
            }

        }
        return obj;
    }
}

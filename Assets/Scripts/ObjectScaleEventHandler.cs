using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;    // For EnhancedTouchSupport
using System;   // For Math

/// <summary>
/// Scale the object as per the finger pinch input
/// </summary>
public class ObjectScaleEventHandler : MonoBehaviour
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

    // Static members
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
        if(_arPlaneController.SpawnedRacetrack == null)
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
        Vector3 currentScale = _arPlaneController.SpawnedRacetrack.transform.localScale;
        if(currentScale.x < 0.5 || currentScale.x > 2.0)
        {
            return;
        }
        Vector3 newScale = currentScale;
        newScale.x *= newScaleRatio;
        newScale.z *= newScaleRatio;
        if(newScale.x < 0.5 || newScale.x > 2.0)
        {
            return;
        }
        _arPlaneController.SpawnedRacetrack.transform.localScale = newScale;
    }
}

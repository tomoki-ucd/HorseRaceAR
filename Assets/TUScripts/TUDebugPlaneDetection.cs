#nullable enable

using UnityEngine;  // Defines Touch and Input
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;  // Defins TrackableType
using System.Collections.Generic;

public class TUDebugPlaneDetection : MonoBehaviour
{
    private ARPlaneManager? _planeManager;
    private ARPlane? _lockedPlane = null;    // Nullable

    [SerializeField]
    private ARRaycastManager? _raycastManager;    // Where is ARRaycastManager in this scene?
                                                // --> It is usually set to XROrigin
    private readonly List<ARRaycastHit> _raycastHits = new List<ARRaycastHit>();  // Why don't it use var declaration?
                                                                        // `var` keyword is only avaialble in local variables.

    // Start is called before the first frame update
    void Start()
    {
        // Get ARPlaneManager
        _planeManager = FindObjectOfType<ARPlaneManager>();  // <T> is Generics
        if (_planeManager == null)
        {
            MyDebugLog("ARlaneManger not found in the scene");
            enabled = false;    // Disable the script if no ARPlaneManager is found. Property in MonoBehavior class
            return;
        }

        // Get ARRaycastManager
        _raycastManager ??= FindObjectOfType<ARRaycastManager>();   // ??= means assigning only when the variable is null
                                                                    // Why ??= only for raycastManager? It's because raycastManager is a [SerializedField].
                                                                    // and it could be already assigned in the Inspector.

        // Check ARRaycastManager
        if(_raycastManager != null)
            MyDebugLog("ARRaycastManager successfully asssigned.") ;
        else
            MyDebugLog("ARCastManager is still null.");


        // Subscribe to plane detection event
        _planeManager.planesChanged += OnPlanesChanged;

    }


    // The default of OnDestroy() method is empty.
    void OnDestroy()
    {
        if (_planeManager != null)
        {
            _planeManager.planesChanged -= OnPlanesChanged;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 0)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);    // Touch is strcut defined in UnityEngine namespace.
                                            // GetTouch(0) means the first touch.

        if(touch.phase != TouchPhase.Began)
        {
            MyDebugLog("touch.phase == TouchPhase.Began --> false");
            return;
        }
        MyDebugLog($"touch.phase == TouchPhase.Began --> true ");

        // Perform a raycast from the screen point
        // bool Raycast(Vector2 screenPoint, List<ARRaycastHit> hitResults, TrackableType that raycast should interact with)
        // Raycast() returns true when it successfully hits one or more trackables.
        MyDebugLog($"touch.position : {touch.position}");
        if(_raycastManager.Raycast(touch.position, _raycastHits, TrackableType.PlaneWithinPolygon))
        {
            MyDebugLog($"Raycast was emitted and hit {_raycastHits.Count} objects."); // Count is property.
            // Get the fist hit
            var hit = _raycastHits[0];

            // Get the corresponding ARPlane
            ARPlane hitPlane = _planeManager!.GetPlane(hit.trackableId);  // hit has trackableId of hit object?

            if(hitPlane != null)
            {
                MyDebugLog($"Hit plane detected: {hitPlane.trackableId}");
                KeepOnlyThisPlane(hitPlane);
            }
            else
            {
                MyDebugLog("No corresponding plane found for the raycast hit.");
            }
        }
    }

    private void MyDebugLog(string message)
    {
        Debug.Log($"[TUDebugPlaneDetection] {message}");
    }


    private void KeepOnlyThisPlane(ARPlane planeToKeep)
    {
        _lockedPlane = planeToKeep;

        // Disable all other planes
        // The following code does not work as TrackableCollection<ARPlane> does not support LINQ methods.
//        foreach (var plane in _planeManager!.trackables.Where(plane => plane != planeToKeep))    // The exclamation mark at the end of _planeManager tells the compiler
//                                                                                                 // that it won't be null and supress the warning message.
        foreach (var plane in _planeManager.trackables)
        {
            if (plane != planeToKeep)
            {
                plane.gameObject.SetActive(false);
            }
        }

        MyDebugLog($"Kept plane: {planeToKeep.trackableId}");

        if (_lockedPlane != null)
        {
            _planeManager.enabled = false;
        }
    }


    // The method to stop detecting new planes
    void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        // Immediately return if the _lockedPlane already exists
        if(_lockedPlane != null) return;

        MyDebugLog($"_planeManager.trackables.count : {_planeManager.trackables.count}");
        if(_planeManager.trackables.count == 0)
            MyDebugLog("No planes detected by ARPlaneManager.");
    }
}

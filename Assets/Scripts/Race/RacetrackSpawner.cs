using UnityEngine;  // Defines Touch and Input
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;  // Defins TrackableType
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Provides the functionality of detecting and selecting ARplanes.
/// </summary>
public class RacetrackSpawner: MonoBehaviour
{
    // Instant fields
    public GameObject racetrackPrefab;
    private ARPlaneManager _planeManager;
    private ARPlane _lockedPlane = null;    // Nullable
    private readonly List<ARRaycastHit> _raycastHits = new List<ARRaycastHit>();
    [SerializeField] private ARRaycastManager _raycastManager;    // ARRaycastManager is attached to XROrign.
    [SerializeField] private Button _setHorseButton;

    public GameObject SpawnedRacetrack{get; set;}


    // Start is called before the first frame update
    void Start()
    {
        // Get ARPlaneManager
        _planeManager = FindObjectOfType<ARPlaneManager>();

        if (_planeManager == null)
        {
            CustomLogger.Print(this, "ARlaneManger not found in the scene");
            enabled = false;    // Disable the script if no ARPlaneManager is found. Property in MonoBehavior class
            return;
        }

        // Get ARRaycastManager
        _raycastManager ??= FindObjectOfType<ARRaycastManager>();   // ??= means assigning only when the destination variable is null

        // Check ARRaycastManager
        if(_raycastManager != null)
            CustomLogger.Print(this, "ARRaycastManager successfully asssigned.") ;
        else
            CustomLogger.Print(this, "ARCastManager is still null.");
    }


    // Update is called once per frame
    void Update()
    {
        if(_lockedPlane != null){
            return;
        }

        if(Input.touchCount == 0)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);    // Touch is strcut defined in UnityEngine namespace.
                                            // GetTouch(0) means the first touch.

        if(touch.phase != TouchPhase.Began)
        {
            return;
        }

        // Perform a raycast from the screen point
        // bool Raycast(Vector2 screenPoint, List<ARRaycastHit> hitResults, TrackableType that raycast should interact with)
        // Raycast() returns true when it successfully hits one or more trackables.
        if(_raycastManager.Raycast(touch.position, _raycastHits, TrackableType.PlaneWithinPolygon))
        {
            CustomLogger.Print(this, $"Raycast was emitted and hit {_raycastHits.Count} objects."); // Count is property.
            // Get the fist hit
            var hit = _raycastHits[0];

            // Get the corresponding ARPlane
            ARPlane hitPlane = _planeManager!.GetPlane(hit.trackableId);  // hit has trackableId of hit object?

            if(hitPlane != null)
            {
                CustomLogger.Print(this, $"Hit plane detected: {hitPlane.trackableId}");
                KeepOnlyThisPlane(hitPlane);
                SpawnRacetrack(hitPlane);
                DisplaySetHorseButton();
            }
            else
            {
                CustomLogger.Print(this, "No corresponding plane found for the raycast hit.");
            }
        }
    }
   

    private void SpawnRacetrack(ARPlane plane)
    {
        Vector3 position = plane.transform.position;
        SpawnedRacetrack = Instantiate(racetrackPrefab, position, Quaternion.identity);
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

        CustomLogger.Print(this, $"Kept plane: {planeToKeep.trackableId}");

        _planeManager.enabled = false;

//        SpawnRacetrack(planeToKeep);
    }

    private void DisplaySetHorseButton()
    {
        _setHorseButton.gameObject.SetActive(true);
    }
}

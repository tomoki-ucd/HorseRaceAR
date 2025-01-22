#nullable enable

using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;

public class TUDebugPlaneDetection : MonoBehaviour
{
    private ARPlaneManager? planeManager;
    private ARPlane? lockedPlane = null;    // Nullable

    // The method to stop detecting new planes
    private void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        if(lockedPlane != null) return;

//        if(args.added != null && args.added.Count > 0)  // If there are newly detected planes
//        {
            foreach(var plane in args.added)
            {
                if(lockedPlane == null)
                {
                    lockedPlane = plane;
                }
                else
                {
                    plane.gameObject.SetActive(false);
                }
            }

            if (lockedPlane != null)
            {
                planeManager.enabled = false;
            }
//        }

        // This condition check seems a redundant, however, it's defensive programming practice.
        // args.added =! seems to make sure that args.added.Count > 0, however, there could be unexpected situation due to a bug.
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get ARPlaneManager
        planeManager = FindObjectOfType<ARPlaneManager>();  // <T> is Generics
        if (planeManager == null)
        {
            Debug.LogError("ARlaneManger not found in the scene");
            enabled = false;    // Disable the script if no ARPlaneManager is found. Property in MonoBehavior class
            return;
        }

        // Subscribe to plane detection event
        planeManager.planesChanged += OnPlanesChanged;
        
    }

    void OnDestroy()
    {
        if (planeManager != null)
        {
            planeManager.planesChanged -= OnPlanesChanged;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Log detected planes
        //for (int i = 0; i < planeManager.trackables.Length; i++)    // This does not work because TrackableCollection<T> does not support indexing
                                                                    // You need to convert TrackableCollection to List
        // var planes = new List<ARPlane>(planeManager.trackables); // Convert TrackableCollection to List
                                                                 // List<T> is included in System.Collections.Genericarg
                                                                 // But After all this does not work because TrakableCollections<T> is not convertable to IEnumerable<T>
        // for (int i = 0; i < planes.Count; i++)    // .Length is for Array, .Count is for List
                                                  // Array is fixed size, List is adjustable size
        if(lockedPlane == null)
        {
            foreach (var plane in planeManager.trackables)
            {
                // Check if planes have been detected
    //            if(plane != null)   // This line is unnecessary as plane will never be null
    //            {
                    // ARPlane plane = planes[i];
                Debug.Log($"Plane detected: {plane.trackableId}, Center: {plane.center}");  // Be carefule that "d" of "Id" is lowercase
    //            }
    //            else    // This "else" will never be executed as plane is always non-null
    //            {
    //                Debug.Log("Planes are not detected yet");
    //            }
            }
        }
        Debug.Log($"Plane has been locked. LockedPlane : {lockedPlane.trackableId}");
    }
}

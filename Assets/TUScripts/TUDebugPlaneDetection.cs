using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;

public class TUDebugPlaneDetection : MonoBehaviour
{
    private ARPlaneManager planeManager;

    // Start is called before the first frame update
    void Start()
    {
        // Get ARPlaneManager
        planeManager = FindObjectOfType<ARPlaneManager>();  // <T> is Generics
        
    }

    // Update is called once per frame
    void Update()
    {
        // Log detected planes
        //for (int i = 0; i < planeManager.trackables.Length; i++)    // This does not work because TrackableCollection<T> does not support indexing
                                                                    // You need to convert TrackableCollection to List
        // var planes = new List<ARPlane>(planeManager.trackables); // Convert TrackableCollection to List
                                                                 // List<T> is included in System.Collections.Generic
                                                                 // But After all this does not work because TrakableCollections<T> is not convertable to IEnumerable<T>
        // for (int i = 0; i < planes.Count; i++)    // .Length is for Array, .Count is for List
                                                  // Array is fixed size, List is adjustable size
         foreach (var plane in planeManager.trackables)
        {
            // ARPlane plane = planes[i];
            Debug.Log($"Plane detected: {plane.trackableId}, Center: {plane.center}");  // Be carefule that "d" of "Id" is lowercase
        }
    }
}

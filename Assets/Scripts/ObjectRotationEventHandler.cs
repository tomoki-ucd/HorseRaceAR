using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectRotationEventHandler : MonoBehaviour
{
    [SerializeField] private Slider _YAxisRotationSlider;
    [SerializeField] private ARPlaneController _arPlaneController;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize UI values
        _YAxisRotationSlider.minValue = -180f;
        _YAxisRotationSlider.maxValue = 180f;
        _YAxisRotationSlider.value = 0f;    // Set its default value to 0
        _YAxisRotationSlider.onValueChanged.AddListener(RotateObject); // Make RotateObject method to subscribe
    }

    void OnDestroy()
    {
        if(_YAxisRotationSlider != null)
        {
            // TO DO: Identify why this line cause an error
//            _YAxisRotationSlider.onValueChanged -= RotateObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Rotate the racetrackPrefab according to the input from the slider.
    /// The slider ranges from -180 to 180. The default value is 0.
    ///</summary>
    private void RotateObject(float angle)
    {
//        _targetObject.rotation = Quaternion.Euler(0f, angle, 0f);   // Euler(x, y, z)
        _arPlaneController.SpawnedRacetrack.transform.rotation = Quaternion.Euler(0f, angle, 0f);   // Euler(x, y, z)

        // Rotate the horses in accordance with the racetrack
//        GameObject _horse = GameObject.Find("Horse(Clone)");    // This is not recommended way to get the access.
//        GameObject _horse = GameObject.FindWithTag("Horse");    // Use Tag to find the object.
        ObjectPlacementEventHandler _objectPlacementEventHandler = FindObjectOfType<ObjectPlacementEventHandler>();
        if(_objectPlacementEventHandler == null)
        {
            CustomLogger.Print(this, "_objectPlacementEventHandler is null.");
            return;
        } 

        GameObject _horse = _objectPlacementEventHandler.SpawnedHorse;
        if(_horse == null)
        {
            CustomLogger.Print(this, $"_horse is null.");
            return;
        }
        _horse.transform.rotation = Quaternion.Euler(0f, angle, 0f); 
    }
}

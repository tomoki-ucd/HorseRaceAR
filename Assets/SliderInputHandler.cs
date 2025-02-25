using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderInputHandler : MonoBehaviour
{
    // UI components
    public Slider _YAxisRotationSlider;

    public TUDebugPlaneDetection planeDetectionHandler;



    // Start is called before the first frame update
    void Start()
    {
        // Initialize UI values
        _YAxisRotationSlider.minValue = -180f;
        _YAxisRotationSlider.maxValue = 180f;
        _YAxisRotationSlider.value = 0f;    // Set its default value to 0
        _YAxisRotationSlider.onValueChanged.AddListener(UpdateRotation); // Make UpdateRotation method to subscribe
        
    }

    void OnDestroy()
    {
//        _YAxisRotationSlider.onValueChanged -= UpdateRotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// Rotate the racetrackPrefab according to the input from the slider
    /// The slider ranges from -180 to 180. The default value is 0.
    /// TODO: How can I set OnDestroy() for this subscriber although this class is not specific to the slider instance
    ///</summary>
    private void UpdateRotation(float angle)
    {
        // The first one is supposed to work
//        _targetObject.rotation = Quaternion.Euler(0f, angle, 0f);   // Euler(x, y, z)
        planeDetectionHandler.SpawnedRacetrack.transform.rotation = Quaternion.Euler(0f, angle, 0f);   // Euler(x, y, z)

        // Rotate the horses in accordance with the racetrack
//        GameObject _horse = GameObject.Find("Horse(Clone)");    // This is not recommended way to get the access.
//        GameObject _horse = GameObject.FindWithTag("Horse");    // Use Tag to find the object. But using property(getter) is better.
        AddHorses addHorses = FindObjectOfType<AddHorses>();
        if(addHorses == null)
        {
            MyDebugLog("addHorses is null.");
            return;
        } 

        GameObject _horse = addHorses.SpawnedHorse;
        if(_horse == null)
        {
            MyDebugLog($"_horse is null.");
            return;
        }
        _horse.transform.rotation = Quaternion.Euler(0f, angle, 0f); 
    }

    private void MyDebugLog(string message)
    {
        Debug.Log($"[{this.GetType().Name}] {message}");
    }

}

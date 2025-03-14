using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Provides the functionality of rotating objects.
/// </summary>
public class RotationController : MonoBehaviour
{
    [SerializeField] private Slider _YAxisRotationSlider;
    [SerializeField] private RacetrackSpawner _racetrackSpawner;

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
    /// </summary>
    /// <param name="angle"> Degree to rotate. It ranges from -180d to 180d. </param>
    private void RotateObject(float angle)
    {
        _racetrackSpawner.SpawnedRacetrack.transform.rotation = Quaternion.Euler(0f, angle, 0f);   // Euler(x, y, z)
    }
}

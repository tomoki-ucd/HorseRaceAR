using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Provides the functionality of spawning horse objects (and disabling/enabling UI elements).
/// </summary>
public class HorseSpawner: MonoBehaviour
{
    [SerializeField] private ARPlaneController _arPlaneController;
    [SerializeField] private GameObject _horsePrefab;
    [SerializeField] private Toggle _toggle;
    GameObject _upButton;
    GameObject _downButton;
    GameObject _slider;

    private const float START_LINE = 0.2f;
    private const float GROUND_HEIGHT = 0.07f;

    private GameObject _spawnedHorse = null;
    public float racetrackMeshHeight;

    public GameObject SpawnedHorse
    {
        get
        {
            return _spawnedHorse;
        }
        private set
        {
            _spawnedHorse = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _upButton = GameObject.Find("UpButton");
        _downButton = GameObject.Find("DownButton");
        _slider = GameObject.Find("Slider");
        _toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnToggleValueChanged(bool isOn)
    {
        ControlUIVisibility(isOn);   // If ControlUIVisibility() is executed after AddHorsesOnRacetarck(), it does not disable the UIs.
        SpawnHorseWhenToggleIsOn(isOn);
    }

    /// <summary>
    /// Spawn horse objects when Toggle is checked.
    /// </summary>
    /// <param name="isOn">
    /// Toggle state
    /// </param>
    /// <remarks>
    /// Calls SpawnHorse()
    /// </remakrs>
    private void SpawnHorseWhenToggleIsOn(bool isOn)
    {
        if(isOn)
        {
            if (_arPlaneController == null)
            {
                CustomLogger.Print(this, "_arPlaneController is null");
                return;
            }
            if (SpawnedHorse != null)
            {
                return;
            }
            SpawnedHorse = SpawnHorse(_horsePrefab, _arPlaneController.SpawnedRacetrack);
        }
    }

    /// <summary>
    ///  Spawn Horse on the racetrack.
    /// </summary>
    /// <param name="horsePrefab"> Horse Prefab </param>
    /// <param name="racetrack"> Racetrack </param>
    /// <returns> Spawned Horse </returns>
    /// <remarks>
    ///  To place the object on the racetrack, add the height of the ractrack to the object's position
    ///  as the pivot of the racetrack is at its bottom.
    /// </remarks>
    private GameObject SpawnHorse(GameObject horsePrefab, GameObject racetrack)
    {
            // Get the heigh offset btw the racetrack and the horse.
            Mesh mesh = racetrack.GetComponent<MeshFilter>().mesh;
            Vector3 meshSize = mesh.bounds.size;

//            // TO DO : racetrackMeshHeight shoud be defined in ARPlaneController.
//            racetrackMeshHeight = meshSize.y;

            // Calculate the start line
            float startLineOffset = (-1) * ((meshSize.x / 2) - START_LINE);
            
            // Compute the spawned object's world position
            Vector3 localPosition = new Vector3(startLineOffset, GROUND_HEIGHT, 0.0f);
            Vector3 worldPosition = racetrack.transform.TransformPoint(localPosition);

            CustomLogger.Print(this, $"racetrack.transform.rotation : {racetrack.transform.rotation}");
//            GameObject spawnedHorse = Instantiate(horsePrefab, worldPosition, Quaternion.identity, racetrack.transform);
            Vector3 eulerAngles = racetrack.transform.eulerAngles;
            eulerAngles.y += 90;
            Quaternion newRotation = Quaternion.Euler(eulerAngles);
            GameObject spawnedHorse = Instantiate(horsePrefab, worldPosition, newRotation, racetrack.transform);

            return spawnedHorse;
    }

    /// <summary>
    /// Controls UI visibility in accordance with the toggle state.
    /// </summary>
    /// <param name="isOn">
    /// Toggle state
    /// </param>
    /// <remarks>
    /// When toggle is checked, hide UI elements.
    /// </remarks>
    private void ControlUIVisibility(bool isOn)
    {
        _upButton.SetActive(!isOn);
        _downButton.SetActive(!isOn);
        _slider.SetActive(!isOn);
    }                             
}

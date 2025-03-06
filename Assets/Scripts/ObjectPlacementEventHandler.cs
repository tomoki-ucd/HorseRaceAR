using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Provides the functionality of placing objects and disabling/enabling UI elements.
/// </summary>
public class ObjectPlacementEventHandler: MonoBehaviour
{
    [SerializeField] private ARPlaneController _arPlaneController;
    [SerializeField] private GameObject _horsePrefab;
    [SerializeField] private Toggle _fixToggle;
    GameObject _upButton;
    GameObject _downButton;
    GameObject _slider;

    private const float START_LINE = 0.2f;

    private GameObject _spawnedHorse = null;
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
        _fixToggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnToggleValueChanged(bool isOn)
    {
        DisableRacetrackControlUIs(isOn);   // If DisableRacetrackControlUIs() is executed after AddHorsesOnRacetarck(), it does not disable the UIs.
        AddHorsesOnRacetrack(isOn);
    }

    private void AddHorsesOnRacetrack(bool isOn)
    {
        if(isOn == false)
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

    ///<summary>
    /// Spawn Horse on the racetrack.
    ///<param name=horsePrefab> Horse Prefab </param>
    ///<param name=racetrack> Racetrack </param>
    ///<return> Spawned Horse </return>
    ///</summary>
    ///<remarks>
    /// To place the object on the racetrack, add the height of the ractrack to the position
    /// as the pivot of the racetrack is at its bottom.
    ///</remarks>
    private GameObject SpawnHorse(GameObject horsePrefab, GameObject racetrack)
    {
            // Get the heigh offset btw the racetrack and the horse.
            Mesh mesh = racetrack.GetComponent<MeshFilter>().mesh;
            Vector3 meshSize = mesh.bounds.size;
            float meshHeight = meshSize.y;

            // Calculate the start line
            float startLineOffset = (-1) * ((meshSize.x / 2) - START_LINE);
            
            // Compute the spawned object's world position
            Vector3 localPosition = new Vector3(startLineOffset, meshHeight, 0.0f);
            Vector3 worldPosition = racetrack.transform.TransformPoint(localPosition);

            GameObject spawnedHorse = Instantiate(horsePrefab, worldPosition, Quaternion.identity, racetrack.transform);

            return spawnedHorse;
    }

    private void DisableRacetrackControlUIs(bool isOn)
    {
        _upButton.SetActive(isOn);
        _downButton.SetActive(isOn);
        _slider.SetActive(isOn);
    }                             
}

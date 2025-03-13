using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Provides the functionality of spawning horse objects (and disabling/enabling UI elements).
/// </summary>
public class HorseSpawner: MonoBehaviour
{
    // Static fields
    private const float START_LINE = 0.2f;
    private const float GROUND_HEIGHT = 0.06f;
    private const float Z_POS_ADJUST = 0.0f;

    // Instant fields
    [SerializeField] private ARPlaneController _arPlaneController;
    [SerializeField] private GameObject _horsePrefab;
    [SerializeField] private Toggle _toggle;
    GameObject _upButton;
    GameObject _downButton;
    GameObject _slider;

    public GameObject[] SpawnedHorses{ get; set;}

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
        SpawnHorsesWhenToggleIsOn(isOn);
    }

    /// <summary>
    /// Spawn horse objects when Toggle is checked.
    /// </summary>
    /// <param name="isOn"> Toggle state </param>
    /// <remarks>
    /// Calls SpawnHorses()
    /// </remakrs>
    private void SpawnHorsesWhenToggleIsOn(bool isOn)
    {
        if(isOn)
        {
            if (_arPlaneController == null)
            {
                CustomLogger.Print(this, "_arPlaneController is null");
                return;
            }
            if (SpawnedHorses != null)
            {
                return;
            }
            SpawnedHorses = SpawnHorses(_horsePrefab);
        }
    }

    /// <summary>
    ///  Spawn horses on the racetrack.
    /// </summary>
    /// <param name="horsePrefab"> Horse Prefab </param>
    /// <returns> Spawned Horse </returns>
    /// <remarks>
    ///  To place the object on the racetrack, add the height of the ractrack to the object's position
    ///  as the pivot of the racetrack is at its bottom.
    /// </remarks>
    private GameObject[] SpawnHorses(GameObject horsePrefab)
    {
        Racetrack racetrack = FindObjectOfType<Racetrack>();

        // Determine the horse's rotation to direct forward alongside the race course.
        Vector3 eulerAngles = racetrack.transform.eulerAngles;
        eulerAngles.y -= 90;
        Quaternion rotation = Quaternion.Euler(eulerAngles);

        // Determine the horse's position
        Mesh mesh = racetrack.GetComponent<MeshFilter>().mesh;
        Vector3 meshSize = mesh.bounds.size;

        // x position (at the start line)
        float xPosition = (meshSize.x / 2) - START_LINE;
        
        // y position
        float yPosition = GROUND_HEIGHT;

        // z position (Devides the width of the racetrack by the number of horses)
        float[] zPositions = new float[Horse.NUM_OF_HORSES];
        for(int i = 0; i < zPositions.Length; i++)
        {
            zPositions[i] = meshSize.z * (-(1.0f / 2) + ((1.0f / zPositions.Length) * (i + 1) + Z_POS_ADJUST));
        }

        GameObject[] horses = new GameObject[Horse.NUM_OF_HORSES];
        for (int i = 0; i < horses.Length; i++)
        {
            // First, make a local position relative to the racetrack
            Vector3 posRelativeToRacetrack = new Vector3(xPosition, yPosition, zPositions[i]);
//            CustomLogger.Print(this, $"{posRelativeToRacetrack}");

            // Then, convert the local position to the world position
            Vector3 worldPosition = racetrack.transform.TransformPoint(posRelativeToRacetrack);

            horses[i] = Instantiate(horsePrefab, worldPosition, rotation, racetrack.transform);
            horses[i].name = $"HorseNo{i}";
        }

        return horses;
    }


    /// <summary>
    /// Controls UI visibility in accordance with the toggle state.
    /// </summary>
    /// <param name="isOn"> Toggle state </param>
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

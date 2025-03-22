using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Provides the functionality of spawning horse objects (and disabling/enabling UI elements).
/// </summary>
public class HorseSpawner: MonoBehaviour
{
    // Instant fields
    [SerializeField] private GameObject _horsePrefab;
//    [SerializeField] private Toggle _setHorseButton;
    [SerializeField] private Button _setHorseButton;
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
        _setHorseButton.onClick.AddListener(OnSetHorseButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnSetHorseButtonClicked()
    {
        SpawnHorsesOnClicked();
    }

    /// <summary>
    /// Spawn horse objects when Toggle is checked.
    /// </summary>
    /// <param name="isOn"> Toggle state </param>
    /// <remarks>
    /// Calls SpawnHorses()
    /// </remakrs>
    private void SpawnHorsesOnClicked()
    {
            if (SpawnedHorses != null)
            {
                return;
            }
            Racetrack racetrack = FindObjectOfType<Racetrack>();
            if (racetrack == null)
            {
                CustomLogger.Print(this, $"racetrack is null!!");
                return;
            }
            SpawnedHorses = SpawnHorses(_horsePrefab);
    }

    /// <summary>
    ///  Spawn horses on the racetrack.
    /// </summary>
    /// <param name="horsePrefab"> Horse Prefab </param>
    /// <returns> Spawned Horse </returns>
    /// <remarks>
    ///  To place horses on the racetrack correctly, 
    ///  it needs to add the height of the racetrack `GROUND_HEIGHT`
    ///  to the horses as the pivot of the racetrack is at its bottom.
    /// </remarks>
    private GameObject[] SpawnHorses(GameObject horsePrefab)
    {
        Racetrack racetrack = FindObjectOfType<Racetrack>();
        if (racetrack == null)
        {
            CustomLogger.Print(this, $"racetrack is null!!");
            return null;
        }
        // Determine the horse's rotation to rotate them toward the goal.
        Vector3 eulerAngles = racetrack.transform.eulerAngles;
        eulerAngles.y -= 90;
        Quaternion rotation = Quaternion.Euler(eulerAngles);

        Vector3 meshSize = racetrack.MeshSize;

        // x position (at the start line)
        float xPosition = (meshSize.x / 2) - Racetrack.START_LINE;
        
        // y position
        float yPosition = Racetrack.GROUND_HEIGHT;

        // z position (z position exits for each horse)
        float[] zPosition = new float[Horse.NUM_OF_HORSES];
        float widthPerHorse = racetrack.Width / Horse.NUM_OF_HORSES;
        for(int i = 0; i < zPosition.Length; i++)
        {
            zPosition[i] = racetrack.ZOffset + (widthPerHorse * (i + 1));
        }

        GameObject[] horses = new GameObject[Horse.NUM_OF_HORSES];
        for (int i = 0; i < horses.Length; i++)
        {
            // First, make a local position relative to the racetrack
            Vector3 posRelativeToRacetrack = new Vector3(xPosition, yPosition, zPosition[i]);

            // Then, convert the local position to the world position
            Vector3 worldPosition = racetrack.transform.TransformPoint(posRelativeToRacetrack);

            horses[i] = Instantiate(horsePrefab, worldPosition, rotation, racetrack.transform);
            horses[i].name = $"HorseNo{i}";
            CustomLogger.Print(this, $"horses[{i}].name = {horses[i].name}");
        }

        return horses;
    }
}

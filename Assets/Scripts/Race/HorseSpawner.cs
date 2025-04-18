using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

/// <summary>
/// Provides the functionality of spawning horse objects (and disabling/enabling UI elements).
/// </summary>
public class HorseSpawner: MonoBehaviour
{
    // Instant fields
    [SerializeField] private GameObject[] _horsePrefabs = new GameObject[3];
    [SerializeField] private RacetrackSpawner _racetrackSpawner;

    public GameObject[] SpawnedHorses{ get; set;}

    // Start is called before the first frame update
    void Start()
    {
        _racetrackSpawner.OnRacetrackSpawned += HandleSpawnedRacetrack;
    }

    void OnDestroy()
    {
        _racetrackSpawner.OnRacetrackSpawned -= HandleSpawnedRacetrack;
    }


    private void HandleSpawnedRacetrack(GameObject spawnedRacetrack)
    {
        CustomLogger.Print(this, "HandleSpawnedRacetrack is called");
        SpawnedHorses = SpawnHorses(_horsePrefabs, spawnedRacetrack);
    }


    /// <summary>
    ///  Spawn horses on the racetrack.
    /// </summary>
    /// <param name="horsePrefab"> Horse Prefab </param>
    /// <param name="racetrackObj"> Spawned Racetrack </param>
    /// <returns> Spawned Horse </returns>
    /// <remarks>
    ///  To place horses on the racetrack correctly, 
    ///  it needs to add the height of the racetrack `GROUND_HEIGHT`
    ///  to the horses as the pivot of the racetrack is at its bottom.
    /// </remarks>
    private GameObject[] SpawnHorses(GameObject[] horsePrefabs, GameObject spawnedRacetrack)
    {
        // Determine the horse's rotation to rotate them toward the goal.
        Vector3 eulerAngles = spawnedRacetrack.transform.eulerAngles;
        eulerAngles.y -= 90;
        Quaternion rotation = Quaternion.Euler(eulerAngles);

        Racetrack racetrack = spawnedRacetrack.GetComponent<Racetrack>();
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

            horses[i] = Instantiate(horsePrefabs[i], worldPosition, rotation, racetrack.transform);
            horses[i].GetComponent<Horse>().horseNumber = i + 1;    // Start from #1 by adding 1 to 0
            horses[i].name = InitializeHorseName(i);
            CustomLogger.Print(this, $"horses[{i}].name = {horses[i].name}");
        }

        return horses;
    }

    /// <summary>
    /// Initialize the horses' names with the names acquired by grok.
    /// </summary>
    /// <param name="i"> int i as index for string[] horse names. </param>
    private string InitializeHorseName(int i)
    {
        string[] names = JsonConvert.DeserializeObject<string[]>(AppData.HorseName);
        return names[i];
    }
}

using UnityEngine;

/// <summary>
/// Represents the racetrack.
/// </summary>
public class Racetrack : MonoBehaviour
{
    // Static fields
    public const float COURSE_DISTANCE = 6.0f;
    public const float START_LINE = 0.2f;
//    public const float GROUND_HEIGHT = 0.06f;
    public const float GROUND_HEIGHT = 0.1f;
    public const float RAIL_WIDTH = 0.1f;

    // Instant fields
    public Vector3 MeshSize{get; private set;}
    public float Width{get; private set;}   // Course width not including rails width
    public float ZOffset{get; private set;}


    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        MeshSize = mesh.bounds.size;

        Width = mesh.bounds.size.z - RAIL_WIDTH * 2;    // Width for the rails on both sides of the course

        ZOffset = -(mesh.bounds.size.z / 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

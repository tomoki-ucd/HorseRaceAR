using UnityEngine;

/// <summary>
/// Represents the racetrack.
/// </summary>
public class Racetrack : MonoBehaviour
{
    // Static fields
    public const float COURSE_DISTANCE = 5.7f;  // Shorten the course distance from 6.0f to fit the race
    public const float START_LINE = 0.2f;
    public const float GROUND_HEIGHT = 0.1f;
    public const float RAIL_WIDTH = 0.1f;

    // Instant fields
    public Vector3 MeshSize{get; private set;}
    public float Width{get; private set;}   // Course width not including rails width
    public float ZOffset{get; private set;}


    void Awake()
    {
        var meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = meshFilter.mesh;
        MeshSize = mesh.bounds.size;
        Width = mesh.bounds.size.z - RAIL_WIDTH * 2;    // Width for the rails on both sides of the course
        ZOffset = -(mesh.bounds.size.z / 2.0f);
    }
}

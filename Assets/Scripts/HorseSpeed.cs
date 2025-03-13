using System;
using UnityEditor.Rendering;

public class HorseSpeed
{
    // Static fields
    const int MIN_SPEED = 8;
    const int MAX_SPEED = 12;

    // Instant fields
    public float EarlyStageSpeed{get;}
    public float MidStageSpeed{get;}
    public float FinalStageSpeed{get;}

    // Constructor
    public HorseSpeed()
    {
        float[] speeds = {EarlyStageSpeed, MidStageSpeed, FinalStageSpeed};

        for(int i = 0; i < speeds.Length; i++) 
        {
            speeds[i] = SetSpeedRandomly();
        }
    }

    /// <summary>
    /// Set speed.
    /// </summary>
    /// <remarks>
    /// Randomly set speed within the MIN-MAX range at each stage (early,mid,final).
    /// </remarks>
    float SetSpeedRandomly()
    {
        int min = MIN_SPEED;
        int max = MAX_SPEED;
        Random rand = new Random();
        float speed = rand.Next(min, max)/10.0f;
        CustomLogger.Print(this, $"speed : {speed}");
        return speed;
    }

}

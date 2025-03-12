using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct HorseSpeed
{
    public float EarlyStageSpeed{get; set;}
    public float MidStageSpeed{get; set;}
    public float FinalStageSpeed{get; set;}

    // Constructor
    public HorseSpeed(float earlyStageSpeed, float midStageSpeed, float finalStageSpeed)
    {
        EarlyStageSpeed = earlyStageSpeed;
        MidStageSpeed = midStageSpeed;
        FinalStageSpeed = finalStageSpeed;
    }

}

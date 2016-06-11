using UnityEngine;
using System.Collections;
public enum MQualityType
{
    VeryLow,
    Low,
    Medium,
    High,
    VeryHigh
}

public class GameQuality
{
    private MQualityType QualityType;


    public void SetQualityType(MQualityType type)
    {
        QualityType = type;
        QualitySettings.SetQualityLevel((int)type);
    }

}

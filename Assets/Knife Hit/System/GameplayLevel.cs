using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameplayLevel
{
    [Header("Info")]
    public TargetLog levelLog;
    public string levelName;
    public int levelNumber;

    [Header("Log Rotation")]
    public float angularSpeed = 45f;
    public float angularSpeedSmooth = 5f;
    public AnimationCurve angularSpeedCurve = new AnimationCurve(new Keyframe[]{
        new Keyframe(.0f, 1f),
        new Keyframe(1f, 1f)
    });

    [Header("Knifes")]
    public int startingAmmo = 5;
    public float[] startingKnifes;
}

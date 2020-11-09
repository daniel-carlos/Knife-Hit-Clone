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

    [Header("Knifes")]
    public int startingAmmo = 5;
    public float[] startingKnifes;
}

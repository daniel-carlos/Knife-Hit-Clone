using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSpriteDatabase : MonoBehaviour
{
    public KnifeSpriteRegister[] knives;
}

[System.Serializable]
public struct KnifeSpriteRegister{
    public string name;
    public string id;
    public int cost;
}
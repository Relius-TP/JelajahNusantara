using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Rarity
{
    public string rarity;

    [Range(0.0f, 100.1f)]
    public float rate;

    public CharDatabase[] reward;
}

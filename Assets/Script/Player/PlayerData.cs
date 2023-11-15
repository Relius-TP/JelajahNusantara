using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="playerData", menuName ="ScriptableObject/Player Data")]
public class PlayerData : ScriptableObject
{
    public bool IsHoldingKey { get; set; }
}

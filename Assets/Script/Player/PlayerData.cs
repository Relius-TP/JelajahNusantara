using UnityEngine;

[CreateAssetMenu(fileName ="playerData", menuName ="ScriptableObject/Player Data")]
public class PlayerData : ScriptableObject
{
    public string hero_name;
    public float hero_speed;
    public float hero_visionRange;
    public int hero_health;

    public bool IsHoldingKey { get; set; }
}

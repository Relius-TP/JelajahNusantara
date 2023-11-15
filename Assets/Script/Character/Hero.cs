using UnityEngine;

[CreateAssetMenu(fileName ="HeroName", menuName ="ScriptableObject/Hero")]
public class Hero : ScriptableObject
{
    public Sprite heroSprite;
    public string heroName = "Default";
    public int hpPoint = 3;
    public float speed = 5f;
    public float vision = 5f;
    public float skillCheckSpeed = 3f;
    public float qteSpeed = 3f;
}

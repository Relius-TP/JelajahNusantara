using System;
using UnityEngine;

public class PotionDetection : MonoBehaviour
{
    //Potion parameter effect value and effect duration seconds
    public static event Action<int> GetHealthPotion;
    public static event Action<float, float> GetSpeedPotion;
    public static event Action<float, float> GetVisionPotion;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("LifePotion"))
        {
            GetHealthPotion?.Invoke(1);
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("SpeedPotion"))
        {
            GetSpeedPotion?.Invoke(2f, 5f);
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("VisionPotion"))
        {
            GetVisionPotion?.Invoke(3f, 5f);
            other.gameObject.SetActive(false);
        }
    }
}
